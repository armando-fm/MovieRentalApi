namespace Api.CustomerEndpoints;

public class CustomerListResponse
{
  public List<CustomerDTO> Customers { get; set; } = new List<CustomerDTO>();
}
