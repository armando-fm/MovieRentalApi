using Api.ApiModels;
using Api.CustomerEndpoints;
using Ardalis.ApiEndpoints;
using Core.Entities;
using Core.Entities.MovieAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.MovieRentEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateMovieRentRequest>.WithActionResult<CreateMovieRentResponse>
    {
        private readonly IMovieRentService _service;

        public Create(IMovieRentService service)
        {
            _service = service;
        }

        [HttpPost(CreateMovieRentRequest.Route)]
        [SwaggerOperation(
            Summary = "Creates a new Movie Rent",
            Description = "Creates a new Movie Rent",
            OperationId = "MovieRent.Create",
            Tags = new[] { "MovieRentEndpoints" })
        ]
        public override async Task<ActionResult<CreateMovieRentResponse>> HandleAsync(CreateMovieRentRequest request, CancellationToken cancellationToken = default)
        {
            var createdMovieRent = await _service.CreateMovieRent(new MovieRent() {
                MovieId = request.MovieId,
                CustomerId = request.CustomerId,
                RentDate = request.RentDate,
            }, cancellationToken);

            var result = createdMovieRent.Value;

            var movieDTO = new MovieDTO(result.MovieId, result.Movie.Title, result.Movie.IndicativeClassification);
            var customerDTO = new CustomerDTO(result.CustomerId, result.Customer.Name, result.Customer.Cpf, result.Customer.BirthDate);

            var response = new CreateMovieRentResponse(result.Id, result.MovieId, result.CustomerId, movieDTO, customerDTO);

            return Ok(response);
        }
    }
}