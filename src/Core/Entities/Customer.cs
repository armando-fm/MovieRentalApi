using SharedKernel;
using SharedKernel.Interfaces;

namespace Core.Entities;

public class Customer: BaseEntity, IAuditableEntity, IAggregateRoot
{
  public Customer()
  {
    this.MovieRents = new HashSet<MovieRent>();
  }

  public Customer(string name, string cpf, DateOnly birthDate)
  {
    Name = name;
    Cpf = cpf;
    BirthDate = birthDate;
  }

  public string Name { get; set; } = string.Empty;
  public string Cpf { get; set; } = String.Empty;
  public virtual ICollection<MovieRent> MovieRents { get; set; }
  public DateOnly BirthDate { get; set; }
  public DateTimeOffset CreatedAt { get; set; }
  public string CreatedBy { get; set; }
  public DateTimeOffset UpdatedAt { get; set; }
  public string UpdatedBy { get; set; }
}
