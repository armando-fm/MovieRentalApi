using Api;
using Api.ApiModels;
using Ardalis.HttpClientTestExtensions;
using Xunit;

namespace MovieRentalApi.FunctionalTests.ControllerApis;

[Collection("Sequential")]
public class MovieCreate : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _client;

  public MovieCreate(CustomWebApplicationFactory<WebMarker> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnsOneMovie()
  {
    var result = await _client.GetAndDeserialize<IEnumerable<MovieDTO>>("/api/movies");

    Assert.Single(result);
    Assert.Contains(result, i => i.Title == SeedData.Movie1.Title);
  }
}
