using System.ComponentModel.DataAnnotations;

namespace Api.CustomerEndpoints;

public class CreateCustomerRequest
{
  [Required]
  public string? Name { get; set; }
  [Required]
  [MinLength(11)]
  [MaxLength(11)]
  public string? Cpf { get; set; }
  [Required]
  public DateOnly Birthdate { get; set; }
}
