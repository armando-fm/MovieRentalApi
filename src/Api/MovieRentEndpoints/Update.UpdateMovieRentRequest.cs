using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.MovieRentEndpoints
{
    public class UpdateMovieRentRequest
    {
        public const string Route = "api/MovieRents"; 
        public int Id { get; set; }
        public DateTimeOffset ReturnDate { get; set; }
    }
}