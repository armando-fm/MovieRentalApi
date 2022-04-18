using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.MovieRentEndpoints
{
    public class MovieRentListResponse
    {
        public List<MovieRentDTO> MovieRents { get; set;} = new List<MovieRentDTO>();
    }
}