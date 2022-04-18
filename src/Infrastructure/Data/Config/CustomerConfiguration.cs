using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
{
  public void Configure(EntityTypeBuilder<Customer> builder)
  {
    builder.Property(p => p.Name)
      .HasMaxLength(200)
      .IsRequired();

    builder.Property(p => p.Cpf)
      .HasMaxLength(11)
      .IsRequired();

    builder.HasIndex(i => i.Cpf).IsUnique();
  }
}
