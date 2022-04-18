using SharedKernel;
using SharedKernel.Interfaces;

namespace Core.Entities.MovieAggregate;

public class Movie: BaseEntity, IAuditableEntity, IAggregateRoot
{
  public Movie()
  {
    MovieRents = new HashSet<MovieRent>();
  }

  public string Title { get; set; }
  public int IndicativeClassification { get; set; }
  public bool Launched { get; set; }

  public virtual ICollection<MovieRent> MovieRents { get; set; }
  public DateTimeOffset CreatedAt { get; set; }
  public string CreatedBy { get; set; }
  public DateTimeOffset UpdatedAt { get; set; }
  public string UpdatedBy { get; set; }

  public Movie(string title, int indicativeClassification, bool launched)
  {
    Title = title ?? throw new ArgumentNullException(nameof(title));
    IndicativeClassification = indicativeClassification;
    Launched = launched;
    MovieRents = new HashSet<MovieRent>();
  }
}
