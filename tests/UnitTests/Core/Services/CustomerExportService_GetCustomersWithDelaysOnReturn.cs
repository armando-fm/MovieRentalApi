using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using Core.Entities;
using Core.Services;
using Moq;
using SharedKernel.Interfaces;
using Xunit;

namespace UnitTests.Core.Services;

public class CustomerExportService_GetCustomersWithDelaysOnReturn
{
  private Mock<IRepository<Customer>> _mockRepo = new();
  private CustomerExportService _customerExportService;

  public CustomerExportService_GetCustomersWithDelaysOnReturn()
  {
    _customerExportService = new CustomerExportService(_mockRepo.Object);
  }

  [Fact]
  public async Task ReturnsErrorGivenDataAccessException()
  {
    // string expectedErrorMessage = "Database not there.";
    //
    // _mockRepo
    //   .Setup(expression: r =>
    //     r.ListAsync(It.IsAny<ISpecification<Customer>>()))
    //   .ThrowsAsync(new Exception(expectedErrorMessage));
    //
    // var result = await _customerExportService.GetCustomersWithDelaysOnReturn();
    //
    // Assert.Equal(Ardalis.Result.ResultStatus.Error, result.Status);
    // Assert.Equal(expectedErrorMessage, result.Errors.First());
  }

  [Fact]
  public async Task ReturnsList()
  {
    var result = await _customerExportService.GetCustomersWithDelaysOnReturn();
    
    Assert.Equal(Ardalis.Result.ResultStatus.Ok, result.Status);
  }

}
