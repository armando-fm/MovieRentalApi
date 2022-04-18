using Ardalis.ApiEndpoints;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.CustomerEndpoints;

public class Delete : EndpointBaseAsync
  .WithRequest<DeleteCustomerRequest>
  .WithoutResult
{
  private readonly IRepository<Customer> _repository;
  private readonly ILogger<Delete> _logger;

  public Delete(IRepository<Customer> repository, ILogger<Delete> logger)
  {
    _repository = repository;
    _logger = logger;
  }
  
  [HttpDelete(DeleteCustomerRequest.Route)]
  [SwaggerOperation(
    Summary = "Deletes a Customer",
    Description = "Deletes a Customer",
    OperationId = "Customer.Delete",
    Tags = new[] { "CustomerEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync([FromRoute] DeleteCustomerRequest request, CancellationToken cancellationToken = new())
  {
    var customerToDelete = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (customerToDelete == null) return NotFound();

    try
    {
      // TODO: verify if customer has movie rents
      await _repository.DeleteAsync(customerToDelete, cancellationToken);

      return NoContent();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error while deleting customer");
      throw;
    }
  }
}
