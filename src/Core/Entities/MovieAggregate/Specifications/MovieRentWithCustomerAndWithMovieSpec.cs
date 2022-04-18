using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Core.Entities.MovieAggregate.Specifications
{
    public class MovieRentWithCustomerAndWithMovieSpec : Specification<MovieRent>
    {

        public MovieRentWithCustomerAndWithMovieSpec()
        {
            Query.Include(x => x.Customer).Include(x => x.Movie);
        }
    }
}