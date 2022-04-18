using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ApiModels;
using Api.CustomerEndpoints;

namespace Api.MovieRentEndpoints
{
    public class UpdateMovieRentResponse
    {
        public UpdateMovieRentResponse(int id, MovieDTO movie, CustomerDTO customer)
        {
            Id = id;
            MovieDTO = movie;
            MovieId = movie.Id;
            CustomerDTO = customer;
            CustomerId = customer.Id;
        }
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int CustomerId { get; set; }
        public MovieDTO MovieDTO { get; set; }
        public CustomerDTO CustomerDTO { get; set; }
    
    }
}