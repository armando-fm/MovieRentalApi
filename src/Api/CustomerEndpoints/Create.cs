using Ardalis.ApiEndpoints;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.CustomerEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateCustomerRequest>
  .WithActionResult<CreateCustomerResponse>
{
  private readonly IRepository<Customer> _repository;
  private readonly ILogger<Create> _logger;

  public Create(IRepository<Customer> repository, ILogger<Create> logger)
  {
    _repository = repository;
    _logger = logger;
  }
  
  [HttpPost("api/Customers")]
  [SwaggerOperation(
    Summary = "Creates a new Customer",
    Description = "Creates a new Customer",
    OperationId = "Customer.Create",
    Tags = new[] { "CustomerEndpoints" })
  ]
  public override async Task<ActionResult<CreateCustomerResponse>> HandleAsync(CreateCustomerRequest request, CancellationToken cancellationToken = new CancellationToken())
  {
    if (request.Name == null)
    {
      return BadRequest();
    }

    var newCustomer = new Customer(request.Name, request.Cpf, request.Birthdate);
    Console.WriteLine("Date....{0}",  request.Birthdate);
    try
    {
      var createdItem = await _repository.AddAsync(newCustomer, cancellationToken);

      var response = new CreateCustomerResponse {
        CustomerDto = new CustomerDTO(
          createdItem.Id, createdItem.Name, createdItem.Cpf, createdItem.BirthDate)
      };

      return Ok(response);
    }
    catch (Exception ex)
    {
      // TODO: do some more handle
      _logger.LogError(ex, "Error while saving customer");
      throw;
    }
  }
}
