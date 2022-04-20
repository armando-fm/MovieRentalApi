using Api.ApiModels;
using Core.Entities.MovieAggregate;
using Core.Entities.MovieAggregate.Specifications;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SharedKernel.Interfaces;

namespace Api.Controllers;

public class MoviesController : BaseApiController
{
  private readonly IRepository<Movie> _repository;
  private readonly ILogger<MoviesController> _logger;

  public MoviesController(IRepository<Movie> repository, ILogger<MoviesController> logger)
  {
    _logger = logger;
    _repository = repository;
  }

  [HttpGet]
  public async Task<IActionResult> List()
  {
    var moviesDTOs = (await _repository.ListAsync())
      .Select(movie => new MovieDTO(movie.Id, movie.Title , movie.IndicativeClassification))
      .ToList();

    var response = new {
      data = moviesDTOs
    };

    return Ok(response);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var movieToDelete = await _repository.GetByIdAsync(id);

    if (movieToDelete == null) {
      return NotFound();
    }

    await _repository.DeleteAsync(movieToDelete);

    return NoContent();
  }

  [HttpPost("import")]
  public async Task<IActionResult> Import(IFormFile file) {
    
    var result = new Dictionary<string, object>();
    var imported = new List<string>();
    var notImported = new List<string>();

    using (var reader = new System.IO.StreamReader(file.OpenReadStream())) {
      var content = await reader.ReadToEndAsync();
      var lines = content.Split("\r\n");

      Console.WriteLine("CSV Content.......");
      Console.WriteLine(content);
      Console.WriteLine(lines[1]);

      // Extract data from csv
      for (var i = 1; i < lines.Count(); i++)
      {
        if (!string.IsNullOrEmpty(lines[i])) {
          var movieDetails = lines[i].Split(';');
          var spec = new CountMoviesByTitleSpec(movieDetails[1]);
          // verify if movie does not yet exist in database
          if (await _repository.CountAsync(spec) == 0) {
            try {
              // import movie
              var movie = new Movie(movieDetails[1], int.Parse(movieDetails[2]), movieDetails[3] != "0");
              await _repository.AddAsync(movie);

              imported.Add($"filme: {movieDetails[1]}, foi importado");

            } catch (Exception ex) {
              var message =  $"filme: {movieDetails[1]} não foi importado devido a um erro: {ex.Message}";
              notImported.Add(message);
              _logger.LogError(ex, message);
            }
          } else {
            notImported.Add($"filme: {movieDetails[1]}, não foi importado porque já existe no banco de dados");
          }
        }        
      }
    }

    result.Add("imported", imported);
    result.Add("notImported", notImported);

    return Ok(result);
  }

  [HttpGet("exportReport")]
  public async Task<IActionResult> ExportReport() {
    
    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    using(var package = new ExcelPackage())
    {
        var sheet = package.Workbook.Worksheets.Add("Sheet 1");
        sheet.Cells["A1:C1"].Merge = true;
        sheet.Cells["A1"].Style.Font.Size = 18f;
        sheet.Cells["A1"].Style.Font.Bold = true;
        sheet.Cells["A1"].Value = "Simple example 1";
        var excelData = package.GetAsByteArray();
        var fileName = "MyWorkbook.xlsx";
        return File(excelData, contentType, fileName);
    }

    return Ok();
  }
}
