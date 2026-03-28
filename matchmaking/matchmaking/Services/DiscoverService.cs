using matchmaking.Domain;
using matchmaking.Repositories;
using matchmaking.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matchmaking.Services
{
    internal class DiscoverService
    {
        private ProfileRepository ProfileRepo;
        private InteractionRepository InteractionRepo;
        private MockCommunityUtil CommunityUtil;
        private LocationUtil LocationUtil;

        public DiscoverService(ProfileRepository profileRepo, InteractionRepository interactionRepo, MockCommunityUtil communityUtil, LocationUtil locationUtil)
        {
            ProfileRepo = profileRepo;
            InteractionRepo = interactionRepo;
            CommunityUtil = communityUtil;
            LocationUtil = locationUtil;
        }

        private double CalculateDistance(string location1, string location2)
        {
            var (lat1, lon1) = LocationUtil.GetCoords(location1);
            var (lat2, lon2) = LocationUtil.GetCoords(location2);

            const double R = 6371;
            double dLat = (lat2 - lat1) * Math.PI / 180;
            double dLon = (lon2 - lon1) * Math.PI / 180;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double CalculateCompatibility(DatingProfile user, DatingProfile candidate)
        {
            if (candidate.Age < user.MinPreferredAge || candidate.Age > user.MaxPreferredAge)
                return 0;
            if (user.Age < candidate.MinPreferredAge || user.Age > candidate.MaxPreferredAge)
                return 0;

            double distance = CalculateDistance(user.Location, candidate.Location);
            if (distance > user.MaxDistance + 10 || distance > candidate.MaxDistance + 10)
                return 0;

            int nc = CommunityUtil.GetSahredCommunities(user.UserId, candidate.UserId).Count;
            int ni = user.Interests.Intersect(candidate.Interests).Count();
            int b1 = user.IsBoosted ? 1 : 0;
            int b2 = candidate.IsBoosted ? 1 : 0;

            double score = b1 * 40 + b2 * 40 + 50 + nc * 7 + ni * 5 - Math.Pow(Math.Floor(distance / 10), 1.7);

            return score <= 0 ? 0 : score;
        }

        public List<DatingProfile> GetCandidates(int profileId)
        {
            DatingProfile user = ProfileRepo.FindById(profileId);
            List<DatingProfile> allProfiles = ProfileRepo.GetAll();
            List<Interaction> interactions = InteractionRepo.GetAll();

            HashSet<int> seenProfiles = interactions
                .Where(i => i.FromProfileId == profileId)
                .Select(i => i.ToProfileId)
                .ToHashSet();

            List<DatingProfile> candidates = allProfiles
                .Where(p =>
                    p.UserId != profileId && !p.IsArchived && !seenProfiles.Contains(p.UserId) 
                    && user.PreferredGenders.Contains(p.Gender) && p.PreferredGenders.Contains(user.Gender)
                )
                .ToList();

            List<DatingProfile> result = new List<DatingProfile>();
            DatingProfile hotSeatProfile = candidates.FirstOrDefault(p => p.IsHotSeat);
            if (hotSeatProfile != null)
            {
                result.Add(hotSeatProfile);
                candidates.Remove(hotSeatProfile);
            }

            
            List<DatingProfile> sorted = candidates
                .Select(p => new { Profile = p, Score = CalculateCompatibility(user, p) })
                .Where(x => x.Score > 0)
                .OrderByDescending(x => x.Score)
                .Select(x => x.Profile)
                .ToList();

            result.AddRange(sorted);
            return result;
        }
    }
}