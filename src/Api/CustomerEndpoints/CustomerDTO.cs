using JetBrains.Annotations;

namespace Api.CustomerEndpoints;

public class CustomerDTO: CreateCustomerDTO
{
  public int Id { get; set; }

  public CustomerDTO(int id, [NotNull] string name, [NotNull] string cpf, DateOnly birthDate) : base(name, cpf, birthDate)
  {
    Id = id;
  }

  public CustomerDTO() : base()
  {
  }
}

public abstract class CreateCustomerDTO
{
  
  protected CreateCustomerDTO()
  {
  }
  protected CreateCustomerDTO(string name, string cpf, DateOnly birthDate)
  {
    Name = name;
    Cpf = cpf;
    BirthDate = birthDate;
  }
  public string Name { get; set; }
  public string Cpf { get; set; }
  public DateOnly BirthDate { get; set; }
}
