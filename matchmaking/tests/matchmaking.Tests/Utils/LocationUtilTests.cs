using matchmaking.Utils;

namespace matchmaking.Tests.Utils;

public class LocationUtilTests
{
    [Fact]
    public void GetAllLocations_WhenCsvIsLoaded_ShouldReturnKnownCities()
    {
        var locationUtil = new LocationUtil();

        var locations = locationUtil.GetAllLocations();

        Assert.Equal(72, locations.Count);
        Assert.Contains("Alba Iulia", locations);
        Assert.Contains("Cluj-Napoca", locations);
        Assert.Contains("Timisoara", locations);
    }

    [Fact]
    public void GetCoords_GivenLocationWithDifferentCasingAndWhitespace_ShouldReturnCoordinates()
    {
        var locationUtil = new LocationUtil();

        var coordinates = locationUtil.GetCoords("  alba iulia ");

        Assert.NotNull(coordinates);
        Assert.Equal(46.0669f, coordinates.Value.lat, 4);
        Assert.Equal(23.57f, coordinates.Value.lon, 4);
    }

    [Theory]
    [InlineData("Atlantis")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void GetCoords_GivenUnknownOrBlankLocation_ShouldReturnNull(string? location)
    {
        var locationUtil = new LocationUtil();

        var coordinates = locationUtil.GetCoords(location!);

        Assert.Null(coordinates);
    }
}
