using matchmaking.Utils;

namespace matchmaking.Tests.Utils;

public class MockCommunityUtilTests
{
    [Fact]
    public void GetUserCommunities_GivenKnownUser_ShouldReturnConfiguredCommunities()
    {
        var communityUtil = new MockCommunityUtil();

        var communities = communityUtil.GetUserCommunities(1);

        Assert.NotNull(communities);
        Assert.Equal(new[] { "Hiking", "Coffee Lovers", "Photography" }, communities);
    }

    [Fact]
    public void GetUserCommunities_GivenUnknownUser_ShouldReturnNull()
    {
        var communityUtil = new MockCommunityUtil();

        var communities = communityUtil.GetUserCommunities(999);

        Assert.Null(communities);
    }

    [Fact]
    public void GetSharedCommunities_GivenUsersWithOverlap_ShouldReturnOnlyCommonEntries()
    {
        var communityUtil = new MockCommunityUtil();

        var sharedCommunities = communityUtil.GetSharedCommunities(1, 4);

        Assert.Equal(new[] { "Photography", "Coffee Lovers" }, sharedCommunities);
    }

    [Fact]
    public void GetSharedCommunities_GivenUnknownUser_ShouldReturnEmptyList()
    {
        var communityUtil = new MockCommunityUtil();

        var sharedCommunities = communityUtil.GetSharedCommunities(1, 999);

        Assert.Empty(sharedCommunities);
    }
}
