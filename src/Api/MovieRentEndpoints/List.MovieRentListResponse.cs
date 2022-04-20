using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.MovieRentEndpoints
{
    public class MovieRentListResponse
    {
        public List<MovieRentDTO> Data { get; set;} = new List<MovieRentDTO>();
    }
}