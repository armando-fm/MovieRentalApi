namespace Api.CustomerEndpoints;

public class CustomerListResponse
{
  public List<CustomerDTO> Data { get; set; } = new List<CustomerDTO>();
}
