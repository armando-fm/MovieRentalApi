using JetBrains.Annotations;

namespace Api.ApiModels;

public class MovieDTO: CreateMovieDTO
{
  
  public int Id { get; set; }

  public MovieDTO(int id, string title, int indicativeClassification) : base(title, indicativeClassification)
  {
    this.Id = id;
  }
}

public abstract class CreateMovieDTO
{
  protected CreateMovieDTO(string title, int indicativeClassification)
  {
    Title = title;
    IndicativeClassification = indicativeClassification;
  }

  public string Title { get; set; }
  public int IndicativeClassification { get; set; }
}
