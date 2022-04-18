using System.ComponentModel.DataAnnotations;

namespace Api.CustomerEndpoints;

public class DeleteCustomerRequest
{
  public const string Route = "api/Customers/{Id:int}";
  public static string BuildRoute(int id) => Route.Replace("{Id:int}", id.ToString());
  [Required]
  public int Id { get; set; }
}
