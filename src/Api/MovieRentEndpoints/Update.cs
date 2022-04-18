using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.MovieRentEndpoints
{
    public class Update : EndpointBaseAsync
        .WithRequest<UpdateMovieRentRequest>
        .WithActionResult<UpdateMovieRentResponse>
    {
        private readonly IMovieRentService _movieRentService;

        public Update(IMovieRentService movieRentService)
        {
            _movieRentService = movieRentService;
        }

        [HttpPut(UpdateMovieRentRequest.Route)]
        [SwaggerOperation(
            Summary = "Updates a Movie Rent",
            Description = "Updates a Movie rent",
            OperationId = "MovieRent.Update",
            Tags = new[] {"MovieRentEndpoints"}
        )]
        public override async Task<ActionResult<UpdateMovieRentResponse>> HandleAsync(UpdateMovieRentRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _movieRentService.UpdateMovieRent(request.Id, request.ReturnDate, cancellationToken);

            return Ok(result.Value);
        }
    }
}