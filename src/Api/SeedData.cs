using Core.Entities;
using Core.Entities.MovieAggregate;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api;

public static class SeedData
{
  public static readonly Movie Movie1 = new("Star Wars: Episode V - The Empire Strikes Back", 9,true);
  
  public static readonly Customer Customer1 = new("Maria Luiza Fagundes", "87722279821", new DateOnly(1994, 6, 26));

  public static void Initialize(IServiceProvider serviceProvider)
  {
    using (var dbContext = new AppDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
    {
      // Look for any TODO items.
      if (dbContext.Customers.Any())
      {
        return;   // DB has been seeded
      }

      PopulateTestData(dbContext);


    }
  }
  public static void PopulateTestData(AppDbContext dbContext)
  {
    
    foreach (var item in dbContext.MovieRents)
    {
      dbContext.Remove(item);
    }
    foreach (var item in dbContext.Movies)
    {
      dbContext.Remove(item);
    }
    foreach (var item in dbContext.Customers)
    {
      dbContext.Remove(item);
    }
    dbContext.SaveChanges();

    dbContext.Movies.Add(Movie1);
    dbContext.Customers.Add(Customer1);
    
    // TODO: add more customers and movies

    dbContext.SaveChanges();
  }
}
