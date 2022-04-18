using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.MovieRentEndpoints
{
    public class DeleteMovieRentRequest
    {
         public const string Route = "api/MovieRents/{Id:int}";
         public static string BuildRoute(int id) => Route.Replace("{Id:int}", id.ToString());
         [Required]
         public int Id { get; set; }
    }
}