using Core.Entities.MovieAggregate;
using Xunit;

namespace IntegrationTests.Data;

public class EfRepositoryDelete : BaseEfRepoTestFixture
{
  [Fact]
  public async Task DeletesItemAfterAddingIt()
  {
    // add a movie
    var repository = GetRepository();
    var initialTitle = Guid.NewGuid().ToString();
    var movie = new Movie(initialTitle, 7, true);
    await repository.AddAsync(movie);

    // delete the item
    await repository.DeleteAsync(movie);

    // verify it's no longer there
    Assert.DoesNotContain(await repository.ListAsync(),
        m => m.Title == initialTitle);
  }
}
