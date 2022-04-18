using Ardalis.EFCore.Extensions;
using Core.Entities;
using Core.Entities.MovieAggregate;
using SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Interfaces;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
  private readonly IMediator? _mediator;

  //public AppDbContext(DbContextOptions options) : base(options)
  //{
  //}

  public AppDbContext(DbContextOptions<AppDbContext> options, IMediator? mediator)
      : base(options)
  {
    _mediator = mediator;
  }
  
  public DbSet<Customer> Customers => Set<Customer>();
  public DbSet<Movie> Movies => Set<Movie>();
  public DbSet<MovieRent> MovieRents => Set<MovieRent>();
  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

    // alternately this is built-in to EF Core 2.2
    //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    OnBeforeSaveChanges();
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_mediator == null) return result;
    
    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
        .Select(e => e.Entity)
        .Where(e => e.Events.Any())
        .ToArray();

    foreach (var entity in entitiesWithEvents)
    {
      var events = entity.Events.ToArray();
      entity.Events.Clear();
      foreach (var domainEvent in events)
      {
        await _mediator.Publish(domainEvent).ConfigureAwait(false);
      }
    }

    return result;
  }

  public override int SaveChanges()
  {
    OnBeforeSaveChanges();
    return SaveChangesAsync().GetAwaiter().GetResult();
  }

  private void OnBeforeSaveChanges()
  {
    var entries = ChangeTracker.Entries();

    foreach (var entityEntry in entries)
    {
      if (entityEntry.Entity is IAuditableEntity auditableEntity)
      {
        var now = DateTimeOffset.Now;
        // TODO: implement http context accessor to get logged in user;
        var username = "unknown";

        switch (entityEntry.State)
        {
          case EntityState.Added:
          {
            auditableEntity.CreatedAt = now;
            auditableEntity.CreatedBy = username;
            auditableEntity.UpdatedAt = now;
            auditableEntity.UpdatedBy = username;
            break;
          }

          case EntityState.Modified:
          {
            auditableEntity.UpdatedAt = now;
            auditableEntity.UpdatedBy = username;
            break;
          }
        }
      }
    }
  }
  
  
}
