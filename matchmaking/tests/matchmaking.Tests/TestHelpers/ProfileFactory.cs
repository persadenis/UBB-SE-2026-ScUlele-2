using matchmaking.Domain;

namespace matchmaking.Tests.TestHelpers;

internal static class ProfileFactory
{
    public static DatingProfile Create(
        int userId = 1,
        string name = "Test User",
        Gender gender = Gender.FEMALE,
        List<Gender>? preferredGenders = null,
        string location = "Alba Iulia",
        string nationality = "Romanian",
        int maxDistance = 100,
        int age = 25,
        int minPreferredAge = 18,
        int maxPreferredAge = 99,
        string bio = "Bio",
        bool displayStarSign = false,
        bool isArchived = false,
        List<Photo>? photos = null,
        List<string>? interests = null,
        DateTime? dateOfBirth = null,
        LoverType? loverType = null,
        bool isHotSeat = false,
        bool isBoosted = false,
        int boostDay = 0,
        int hotSeatDay = 0)
    {
        return new DatingProfile(
            userId,
            name,
            gender,
            preferredGenders ?? new List<Gender> { Gender.MALE, Gender.FEMALE, Gender.NON_BINARY, Gender.OTHER },
            location,
            nationality,
            maxDistance,
            age,
            minPreferredAge,
            maxPreferredAge,
            bio,
            displayStarSign,
            isArchived,
            photos ?? new List<Photo>(),
            interests ?? new List<string>(),
            dateOfBirth ?? new DateTime(1999, 1, 1),
            loverType,
            isHotSeat,
            isBoosted,
            boostDay,
            hotSeatDay);
    }
}
