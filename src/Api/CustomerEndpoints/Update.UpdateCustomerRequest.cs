using System.ComponentModel.DataAnnotations;

namespace Api.CustomerEndpoints;

public class UpdateCustomerRequest
{
  public const string Route = "api/Customers";
  [Required]
  public int Id { get; set; }
  [Required]
  public string? Name { get; set; }
  [Required]
  public DateOnly Birthdate { get; set; }
  [MinLength(11)]
  [MaxLength(11)]
  [Required] public string? Cpf { get; set; }
}
