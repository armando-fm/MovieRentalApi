using JetBrains.Annotations;

namespace Api.CustomerEndpoints;

public class CustomerDTO: CreateCustomerDTO
{
  public int Id { get; set; }

  public CustomerDTO(int id, [NotNull] string name, [NotNull] string cpf, DateOnly birtDate) : base(name, cpf, birtDate)
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
  protected CreateCustomerDTO(string name, string cpf, DateOnly birtDate)
  {
    Name = name;
    Cpf = cpf;
    BirtDate = birtDate;
  }
  public string Name { get; set; }
  public string Cpf { get; set; }
  public DateOnly BirtDate { get; set; }
}
