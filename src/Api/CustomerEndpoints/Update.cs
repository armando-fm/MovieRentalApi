using Ardalis.ApiEndpoints;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.CustomerEndpoints;

public class Update: EndpointBaseAsync
  .WithRequest<UpdateCustomerRequest>
  .WithActionResult<UpdateCustomerResponse>
{
  private readonly IRepository<Customer> _repository;
  private readonly ILogger<Update> _logger;

  public Update(IRepository<Customer> repository, ILogger<Update> logger)
  {
    _repository = repository;
    _logger = logger;
  }
  
  [HttpPut(UpdateCustomerRequest.Route)]
  [SwaggerOperation(
    Summary = "Updates a Customer",
    Description = "Updates a Customer",
    OperationId = "Customer.Update",
    Tags = new[] { "CustomerEndpoints" })
  ]
  public override async Task<ActionResult<UpdateCustomerResponse>> HandleAsync(UpdateCustomerRequest request, CancellationToken cancellationToken = new CancellationToken())
  {
    if (request.Name == null || request.Cpf == null)
    {
      return BadRequest();
    }
    var existingCustomer = await _repository.GetByIdAsync(request.Id, cancellationToken);

    if (existingCustomer == null)
    {
      return NotFound();
    }
    existingCustomer.Name = request.Name;
    existingCustomer.Cpf = request.Cpf;
    existingCustomer.BirthDate = request.Birthdate;

    try
    {
      await _repository.UpdateAsync(existingCustomer, cancellationToken);

      var response = new UpdateCustomerResponse
      {
        CustomerDto = new CustomerDTO(
          existingCustomer.Id, existingCustomer.Name, existingCustomer.Cpf, existingCustomer.BirthDate)
      };

      return Ok(response);
    } catch (Exception ex)
    {
      _logger.LogError(ex, "Error while updating customer");
      throw;
    }
  }
}
