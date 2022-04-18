using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.MovieRentEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteMovieRentRequest>.WithoutResult
    {
        private readonly IRepository<MovieRent> _repository;

        public Delete(IRepository<MovieRent> repository)
        {
            _repository = repository;
            
        }

        [HttpDelete(DeleteMovieRentRequest.Route)]
        [SwaggerOperation(
            Summary = "Deletes a Movie rent",
            Description = "Deletes a Movie rent",
            OperationId = "MovieRent.Delete",
            Tags = new[] { "MovieRentEndpoints" })
        ]
        public override async Task<ActionResult> HandleAsync([FromRoute]DeleteMovieRentRequest request, CancellationToken cancellationToken = default)
        {
            var movierentToRemove = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (movierentToRemove == null) {
                return NotFound();
            }

            await _repository.DeleteAsync(movierentToRemove, cancellationToken);

            return NoContent(); 
        }
    }
}