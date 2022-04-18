using Api.ApiModels;
using Core.Entities.MovieAggregate;
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

    return Ok(moviesDTOs);
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
          Console.WriteLine(movieDetails[0]);
          // verify if movie does not yet exist in database
          if (await _repository.GetByIdAsync(int.Parse(movieDetails[0])) == null) {
            try {
              // import movie
              var movie = new Movie(movieDetails[1], int.Parse(movieDetails[2]), movieDetails[3] != "0");
              await _repository.AddAsync(movie);

              imported.Add($"movie: {movieDetails[1]}, has been imported");

            } catch (Exception ex) {
              var message =  $"could not import movie {movieDetails[1]} due to error: {ex.Message}";
              notImported.Add(message);
              _logger.LogError(ex, message);
            }
          } else {
            notImported.Add($" could not import movie {movieDetails[1]} because it is already in the database");
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
