using Api.ApiModels;
using Core.Entities.MovieAggregate;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;

namespace Api.Controllers;

public class MoviesController : BaseApiController
{
  private readonly IRepository<Movie> _repository;

  public MoviesController(IRepository<Movie> repository)
  {
    _repository = repository;
  }

  [HttpGet]
  public async Task<IActionResult> List()
  {
    var moviesDTOs = (await _repository.ListAsync())
      .Select(movie => new MovieDTO(movie.Id, movie.Title , movie.IndicativeClassification))
      .ToList();

    return Ok(moviesDTOs);
  }
}
