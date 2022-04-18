using Ardalis.ApiEndpoints;
using Core.Entities;
using Core.Entities.MovieAggregate.Specifications;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.MovieRentEndpoints
{
    public class List : EndpointBaseAsync
                        .WithoutRequest
                        .WithActionResult<MovieRentListResponse>
    {
        private readonly IRepository<MovieRent> _repository;
        public List(IRepository<MovieRent> repository)
        {
            _repository = repository;
        }

        [HttpGet("api/MovieRents")]
        [SwaggerOperation(
            Summary = "Gets a list of all movie rents",
            Description = "Gets a list of all movie rents",
            OperationId = "MovieRent.List",
            Tags = new[] {"MovieRentEndpoints"}
        )]
        public override async Task<ActionResult<MovieRentListResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var response = new MovieRentListResponse();
            var spec = new MovieRentWithCustomerAndWithMovieSpec();
            
            response.MovieRents = (
                await _repository
                .ListAsync(spec, cancellationToken))
                .Select(movieRent =>
                    new MovieRentDTO(
                        movieRent.Id,
                        movieRent.CustomerId,
                        movieRent.MovieId,
                        movieRent.RentDate,
                        movieRent.Movie,
                        movieRent.Customer))
                .ToList();
            
            return Ok(response);
        }
    }
}