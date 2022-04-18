using Api.ApiModels;
using Api.CustomerEndpoints;
using Core.Entities;
using Core.Entities.MovieAggregate;

namespace Api.MovieRentEndpoints
{
    public class MovieRentDTO : CreateMovieRentDTO
    {
        public MovieRentDTO(int id, int customerId, int movieId, DateTimeOffset rentDate, Movie movie, Customer customer) : base(customerId, movieId, rentDate)
        {
            Id = id;
            Movie = new MovieDTO(movie.Id, movie.Title, movie.IndicativeClassification) ;
            Customer = new CustomerDTO(customer.Id, customer.Name, customer.Cpf, customer.BirthDate);
        }

        
        public int Id { get; set; }
        public MovieDTO Movie { get; set; }
        public CustomerDTO Customer { get; set; }
    }

    public abstract class CreateMovieRentDTO {
        protected CreateMovieRentDTO(int customerId, int movieId, DateTimeOffset rentDate)
        {
            CustomerId = customerId;
            MovieId = movieId;
            RentDate = rentDate;
        }

        public int MovieId { get; set;}
        public int CustomerId { get; set;}
        public DateTimeOffset RentDate { get; set; }
    }
}