using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Api;

public class Program
{
  public static void Main(string[] args)
  {
    
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

    builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

    var host = builder.Configuration["DB_HOST"] ?? "localhost";
    var port = builder.Configuration["DB_PORT"] ?? "3306";
    var password = builder.Configuration["DB_PASSWORD"] ?? "p@55w0rd";
    var db = builder.Configuration["DB_NAME"] ?? "movie_rent_db";

    string connectionString = $"server={host};userid=root;pwd={password};port={port};database={db};";

    builder.Services.AddDbContext(connectionString);
    builder.Services
      .AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
      .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters())
      .AddNewtonsoftJson();

    const string corsPolicy = "CorsPolicy";
    builder.Services.AddCors(options =>
    {
      options.AddPolicy(name: corsPolicy,
        b =>
        {
          //builder.WithOrigins(baseUrlConfig.WebBase.Replace("host.docker.internal", "localhost").TrimEnd('/'));
          //TODO: Fix this
          b.AllowAnyOrigin();
          b.AllowAnyMethod();
          b.AllowAnyHeader();
        });
    });

    builder.Services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
      c.EnableAnnotations();
      c.UseDateOnlyTimeOnlyStringConverters();
    });

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
    builder.Services.Configure<ServiceConfig>(config =>
    {
      config.Services = new List<ServiceDescriptor>(builder.Services);

      // optional - default path to view services is /listallservices - recommended to choose your own path
      config.Path = "/listservices";
    });


    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
      containerBuilder.RegisterModule(new DefaultCoreModule());
      containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
    });
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
      app.UseShowAllServicesMiddleware();
    }

    app.UseRouting();

    app.UseCors(corsPolicy);

    app.UseHttpsRedirection();

// Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

    app.UseEndpoints(endpoints =>
    {
      endpoints.MapControllers();
    });

// Seed Database
    app.Logger.LogInformation("PublicApi App created...");

    app.Logger.LogInformation("Seeding Database...");

    using (var scope = app.Services.CreateScope())
    {
      var services = scope.ServiceProvider;

      try
      {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        context.Database.EnsureCreated();
        SeedData.Initialize(services);
      }
      catch (Exception ex)
      {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
      }
    }

    app.Logger.LogInformation("LAUNCHING Api");
    app.Run();
    Console.WriteLine("Hello World!");
  }
}
