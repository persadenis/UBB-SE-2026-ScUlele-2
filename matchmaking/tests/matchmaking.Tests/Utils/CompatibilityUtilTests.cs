using matchmaking.Domain;
using matchmaking.Tests.TestHelpers;
using matchmaking.Utils;

namespace matchmaking.Tests.Utils;

public class CompatibilityUtilTests
{
    [Fact]
    public void CalculateCompatibility_GivenCompatibleProfiles_ShouldReturnExpectedPositiveScore()
    {
        var compatibilityUtil = new CompatibilityUtil();
        var profilePair = CreateCompatibleProfilePair();

        var score = compatibilityUtil.CalculateCompatibility(profilePair.firstProfile, profilePair.secondProfile);

        Assert.Equal(74f, score, 3);
    }

    [Fact]
    public void CalculateCompatibility_GivenBoostedProfile_ShouldIncreaseScoreByForty()
    {
        var compatibilityUtil = new CompatibilityUtil();
        var baselinePair = CreateCompatibleProfilePair();
        var boostedPair = CreateCompatibleProfilePair(secondIsBoosted: true);

        var baselineScore = compatibilityUtil.CalculateCompatibility(baselinePair.firstProfile, baselinePair.secondProfile);
        var boostedScore = compatibilityUtil.CalculateCompatibility(boostedPair.firstProfile, boostedPair.secondProfile);

        Assert.Equal(40f, boostedScore - baselineScore, 3);
    }

    [Fact]
    public void CalculateCompatibility_GivenProfileWithUnknownLocation_ShouldReturnZero()
    {
        var compatibilityUtil = new CompatibilityUtil();
        var firstProfile = ProfileFactory.Create(
            userId: 1,
            location: "Alba Iulia",
            age: 28,
            minPreferredAge: 24,
            maxPreferredAge: 35,
            interests: new List<string> { "photography", "coffee" });
        var secondProfile = ProfileFactory.Create(
            userId: 4,
            location: "Atlantis",
            age: 29,
            minPreferredAge: 24,
            maxPreferredAge: 35,
            interests: new List<string> { "photography", "coffee" });

        var score = compatibilityUtil.CalculateCompatibility(firstProfile, secondProfile);

        Assert.Equal(0f, score);
    }

    [Fact]
    public void CalculateCompatibility_GivenProfileOutsidePreferredAgeRange_ShouldReturnZero()
    {
        var compatibilityUtil = new CompatibilityUtil();
        var firstProfile = ProfileFactory.Create(
            userId: 1,
            location: "Alba Iulia",
            age: 40,
            minPreferredAge: 24,
            maxPreferredAge: 45,
            interests: new List<string> { "photography", "coffee" });
        var secondProfile = ProfileFactory.Create(
            userId: 4,
            location: "Alba Iulia",
            age: 29,
            minPreferredAge: 18,
            maxPreferredAge: 35,
            interests: new List<string> { "photography", "coffee" });

        var score = compatibilityUtil.CalculateCompatibility(firstProfile, secondProfile);

        Assert.Equal(0f, score);
    }

    [Fact]
    public void CalculateCompatibility_GivenProfilesBeyondDistanceThreshold_ShouldReturnZero()
    {
        var compatibilityUtil = new CompatibilityUtil();
        var firstProfile = ProfileFactory.Create(
            userId: 1,
            location: "Satu Mare",
            maxDistance: 20,
            age: 28,
            minPreferredAge: 24,
            maxPreferredAge: 35,
            interests: new List<string> { "photography", "coffee" });
        var secondProfile = ProfileFactory.Create(
            userId: 4,
            location: "Constanta",
            maxDistance: 20,
            age: 29,
            minPreferredAge: 24,
            maxPreferredAge: 35,
            interests: new List<string> { "photography", "coffee" });

        var score = compatibilityUtil.CalculateCompatibility(firstProfile, secondProfile);

        Assert.Equal(0f, score);
    }

    private static (DatingProfile firstProfile, DatingProfile secondProfile) CreateCompatibleProfilePair(bool secondIsBoosted = false)
    {
        var firstProfile = ProfileFactory.Create(
            userId: 1,
            name: "Ana",
            location: "Alba Iulia",
            maxDistance: 100,
            age: 28,
            minPreferredAge: 24,
            maxPreferredAge: 35,
            interests: new List<string> { "photography", "coffee", "hiking" });

        var secondProfile = ProfileFactory.Create(
            userId: 4,
            name: "Mara",
            gender: Gender.MALE,
            location: "Alba Iulia",
            maxDistance: 100,
            age: 29,
            minPreferredAge: 24,
            maxPreferredAge: 35,
            interests: new List<string> { "photography", "coffee", "travel" },
            isBoosted: secondIsBoosted);

        return (firstProfile, secondProfile);
    }
}
