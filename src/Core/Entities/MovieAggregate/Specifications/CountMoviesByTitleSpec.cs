using Ardalis.Specification;

namespace Core.Entities.MovieAggregate.Specifications
{
    public class CountMoviesByTitleSpec : Specification<Movie>
    {

        public CountMoviesByTitleSpec(string title)
        {
            Query.Where(movie => movie.Title == title);
        }
    }
}