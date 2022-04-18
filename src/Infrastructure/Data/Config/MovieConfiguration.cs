using Core.Entities.MovieAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
  public void Configure(EntityTypeBuilder<Movie> builder)
  {
    builder.Property(p => p.Title)
      .HasMaxLength(100)
      .IsRequired();
  }
}
