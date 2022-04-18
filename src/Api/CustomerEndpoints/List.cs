using Ardalis.ApiEndpoints;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.CustomerEndpoints;

public class List : EndpointBaseAsync.WithoutRequest.WithActionResult<CustomerListResponse>
{
  private readonly IRepository<Customer> _repository;

  public List(IRepository<Customer> repository)
  {
    _repository = repository;
  }
  
  [HttpGet("api/Customers")]
  [SwaggerOperation(
    Summary = "Gets a list of all customers",
    Description = "Gets a list of all customers",
    OperationId = "Customer.List",
    Tags = new[] { "CustomerEndpoints" })
  ]
  public override async Task<ActionResult<CustomerListResponse>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    var response = new CustomerListResponse();

    response.Customers = (await _repository.ListAsync(cancellationToken))
      .Select(customer => new CustomerDTO(customer.Id, customer.Name, customer.Cpf, customer.BirthDate))
      .ToList();

    return Ok(response);
  }
}
