using System.ComponentModel.DataAnnotations;
using Api.ApiModels;
using Api.CustomerEndpoints;

namespace Api.MovieRentEndpoints
{
    public class CreateMovieRentResponse
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int CustomerId { get; set; }
        public MovieDTO MovieDTO { get; set; }
        public CustomerDTO CustomerDTO { get; set; }

        public CreateMovieRentResponse(int id, int movieId, int customerId, MovieDTO movieDTO, CustomerDTO customerDTO)
        {
            Id = id;
            MovieId = movieId;
            CustomerId = customerId;
            MovieDTO = movieDTO;
            CustomerDTO = customerDTO;
        }
    }
}