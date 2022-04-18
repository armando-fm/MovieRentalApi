using Core.Entities.MovieAggregate;
using SharedKernel;
using SharedKernel.Interfaces;

namespace Core.Entities;

public class MovieRent: BaseEntity, IAggregateRoot
{
  public DateTimeOffset RentDate { get; set; }
  public DateTimeOffset ReturnDate { get; set; }
  public int MovieId { get; set; }
  public virtual Movie Movie { get; set; }
  public int CustomerId { get; set; }
  public virtual Customer Customer { get; set; }
}
