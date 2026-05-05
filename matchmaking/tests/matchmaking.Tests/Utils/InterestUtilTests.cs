using matchmaking.Utils;

namespace matchmaking.Tests.Utils;

public class InterestUtilTests
{
    [Fact]
    public void GetAll_WhenCsvIsLoaded_ShouldReturnNormalizedInterests()
    {
        var interestUtil = new InterestUtil();

        var interests = interestUtil.GetAll();

        Assert.Equal(48, interests.Count);
        Assert.Contains("travel", interests);
        Assert.Contains("tv shows", interests);
        Assert.Contains("board games", interests);
        Assert.All(interests, interest => Assert.Equal(interest, interest.Trim().ToLowerInvariant()));
        Assert.DoesNotContain(interests, string.IsNullOrWhiteSpace);
    }
}
