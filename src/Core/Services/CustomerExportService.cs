using Ardalis.Result;
using Core.Entities;
using Core.Interfaces;
using SharedKernel.Interfaces;

namespace Core.Services;

public class CustomerExportService : ICustomerExportService
{
  private readonly IRepository<Customer> _repository;

  public CustomerExportService(IRepository<Customer> repository)
  {
    _repository = repository;
  }

  public Task<Result<List<Customer>>> GetCustomersWithDelaysOnReturn()
  {
    throw new NotImplementedException();
  }

  public Task<Result<Customer>> GetSecondCustomerWithMoreMovieRents()
  {
    throw new NotImplementedException();
  }
}
