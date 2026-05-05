using matchmaking.Domain;
using matchmaking.Tests.TestHelpers;

namespace matchmaking.Tests.Domain;

public class DatingProfileTests
{
    [Theory]
    [InlineData(1, 19, (int)StarSign.CAPRICORN)]
    [InlineData(1, 20, (int)StarSign.AQUARIUS)]
    [InlineData(2, 18, (int)StarSign.AQUARIUS)]
    [InlineData(2, 19, (int)StarSign.PISCES)]
    [InlineData(3, 20, (int)StarSign.PISCES)]
    [InlineData(3, 21, (int)StarSign.ARIES)]
    [InlineData(4, 19, (int)StarSign.ARIES)]
    [InlineData(4, 20, (int)StarSign.TAURUS)]
    [InlineData(5, 20, (int)StarSign.TAURUS)]
    [InlineData(5, 21, (int)StarSign.GEMINI)]
    [InlineData(6, 20, (int)StarSign.GEMINI)]
    [InlineData(6, 21, (int)StarSign.CANCER)]
    [InlineData(7, 22, (int)StarSign.CANCER)]
    [InlineData(7, 23, (int)StarSign.LEO)]
    [InlineData(8, 22, (int)StarSign.LEO)]
    [InlineData(8, 23, (int)StarSign.VIRGO)]
    [InlineData(9, 22, (int)StarSign.VIRGO)]
    [InlineData(9, 23, (int)StarSign.LIBRA)]
    [InlineData(10, 22, (int)StarSign.LIBRA)]
    [InlineData(10, 23, (int)StarSign.SCORPIO)]
    [InlineData(11, 21, (int)StarSign.SCORPIO)]
    [InlineData(11, 22, (int)StarSign.SAGITTARIUS)]
    [InlineData(12, 21, (int)StarSign.SAGITTARIUS)]
    [InlineData(12, 22, (int)StarSign.CAPRICORN)]
    public void GetStarSign_GivenBoundaryDate_ShouldReturnExpectedSign(int month, int day, int expected)
    {
        var profile = ProfileFactory.Create(dateOfBirth: new DateTime(2000, month, day));

        var result = profile.GetStarSign();

        Assert.Equal((StarSign)expected, result);
    }
}
