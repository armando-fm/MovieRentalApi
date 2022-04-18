using Ardalis.Result;
using Core.Entities;

namespace Core.Interfaces;

public interface ICustomerExportService
{
  Task<Result<List<Customer>>> GetCustomersWithDelaysOnReturn();
  Task<Result<Customer>> GetSecondCustomerWithMoreMovieRents();
}
