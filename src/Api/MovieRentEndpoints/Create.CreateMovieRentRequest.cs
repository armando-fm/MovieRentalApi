using System.ComponentModel.DataAnnotations;

namespace Api.MovieRentEndpoints
{
    public class CreateMovieRentRequest
    {
        
        public const string Route = "api/MovieRents";

        [Required]
        public int MovieId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public DateTimeOffset RentDate { get; set; }
    }
}