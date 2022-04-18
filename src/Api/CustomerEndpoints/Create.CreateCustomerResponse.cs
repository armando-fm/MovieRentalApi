using System.ComponentModel.DataAnnotations;

namespace Api.CustomerEndpoints;

public class CreateCustomerResponse
{
  public CustomerDTO CustomerDto = new CustomerDTO();
}
