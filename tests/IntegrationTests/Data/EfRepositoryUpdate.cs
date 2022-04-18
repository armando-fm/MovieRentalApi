using Core.Entities.MovieAggregate;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntegrationTests.Data;

public class EfRepositoryUpdate : BaseEfRepoTestFixture
{
  [Fact]
  public async Task UpdatesItemAfterAddingIt()
  {
    // add a movie
    var repository = GetRepository();
    var initialTitle = Guid.NewGuid().ToString();
    var movie = new Movie(initialTitle, 7, true);

    await repository.AddAsync(movie);

    // detach the item so we get a different instance
    _dbContext.Entry(movie).State = EntityState.Detached;

    // fetch the item and update its title
    var newMovie = (await repository.ListAsync())
        .FirstOrDefault(m => m.Title == initialTitle);
    if (newMovie == null)
    {
      Assert.NotNull(newMovie);
      return;
    }
    Assert.NotSame(movie, newMovie);
    var newTitle = Guid.NewGuid().ToString();
    newMovie.Title = newTitle;
    // Update the item
    await repository.UpdateAsync(newMovie);

    // Fetch the updated item
    var updatedItem = (await repository.ListAsync())
        .FirstOrDefault(m => m.Title == newTitle);

    Assert.NotNull(updatedItem);
    Assert.NotEqual(movie.Title, updatedItem?.Title);
    Assert.Equal(movie.IndicativeClassification, updatedItem?.IndicativeClassification);
    Assert.Equal(movie.Launched, updatedItem?.Launched);
    Assert.Equal(newMovie.Id, updatedItem?.Id);
  }
}
