using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using Core.Entities;
using Core.Entities.MovieAggregate;
using Core.Interfaces;
using SharedKernel.Interfaces;

namespace Core.Services
{
    public class MovieRentCreateService : IMovieRentService
    {
        private readonly IRepository<MovieRent> _repository;
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<Customer> _customerRepository;
        public MovieRentCreateService(IRepository<MovieRent> repository, IRepository<Movie> movieRepository, IRepository<Customer> customerRepository)
        {
            _repository = repository;
            _movieRepository = movieRepository;
            _customerRepository = customerRepository;

            
        }
        public async Task<Result<MovieRent>> CreateMovieRent(MovieRent movieRent, CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.GetByIdAsync(movieRent.CustomerId);

            if (customer == null) {
                return Result<MovieRent>.NotFound();
            }

            
            var movie = await _movieRepository.GetByIdAsync(movieRent.MovieId);

            if (movie == null) {
                return Result<MovieRent>.NotFound();
            }

            var newMovieRent = new MovieRent(){
                Movie = movie,
                Customer = customer,
                RentDate = movieRent.RentDate,
            };

            var createdItem = await _repository.AddAsync(newMovieRent, cancellationToken);

            return new Result<MovieRent>(createdItem);
        }

        public async Task<Result<MovieRent>> UpdateMovieRent(int id, DateTimeOffset returnDate, CancellationToken cancellationToken = default)
        {
            var movieRent = await _repository.GetByIdAsync(id);

            if (movieRent == null) {
                return Result<MovieRent>.NotFound();
            }

            if (returnDate == null) {
                return Result<MovieRent>.Error("Invalid date provided");
            }

            movieRent.ReturnDate = returnDate;

            await _repository.UpdateAsync(movieRent, cancellationToken);

            return new Result<MovieRent>(movieRent);
        }
    }
}