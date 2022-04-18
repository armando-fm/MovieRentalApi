namespace Api.CustomerEndpoints;

public class CustomerListResponse
{
  public List<CustomerDTO> CustomerDTOs { get; set; } = new List<CustomerDTO>();
}
