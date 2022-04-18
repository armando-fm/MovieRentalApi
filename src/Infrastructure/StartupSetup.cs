using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class StartupSetup
{
  public static void AddDbContext(this IServiceCollection services, string connectionString) =>
      services.AddDbContext<AppDbContext>(options =>
      {
        options.UseMySql(connectionString,
          new MySqlServerVersion(new Version(8, 0, 28)),
          mysqlOptions => mysqlOptions.EnableRetryOnFailure(
            5,
            System.TimeSpan.FromSeconds(30),
            null));
      });
}
