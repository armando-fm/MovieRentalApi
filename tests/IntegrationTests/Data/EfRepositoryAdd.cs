using Core.Entities.MovieAggregate;
using Xunit;

namespace IntegrationTests.Data;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
  [Fact]
  public async Task AddsProjectAndSetsId()
  {
    var testMovieTitle = "testMovie";
    var testMovieClassification = 7;
    var testMovieLaunched = true;
    var repository = GetRepository();
    var project = new Movie(testMovieTitle, testMovieClassification, testMovieLaunched);

    await repository.AddAsync(project);

    var newMovie = (await repository.ListAsync())
                    .FirstOrDefault();

    Assert.Equal(testMovieTitle, newMovie?.Title);
    Assert.Equal(testMovieClassification, newMovie?.IndicativeClassification);
    Assert.Equal(testMovieLaunched, newMovie?.Launched);
    Assert.True(newMovie?.Id > 0);
  }
}
