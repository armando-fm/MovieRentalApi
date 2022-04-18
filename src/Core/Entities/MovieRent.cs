using Core.Entities.MovieAggregate;
using SharedKernel;

namespace Core.Entities;

public class MovieRent: BaseEntity
{
  public DateTimeOffset RentDate { get; set; }
  public DateTimeOffset ReturnDate { get; set; }
  public int MovieId { get; set; }
  public virtual Movie Movie { get; set; }
  public int CustomerId { get; set; }
  public virtual Customer Customer { get; set; }
}
