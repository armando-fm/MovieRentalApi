using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IMovieRentService
    {
        Task<Result<MovieRent>>  CreateMovieRent(MovieRent movieRent, CancellationToken cancellationToken);
        Task<Result<MovieRent>>  UpdateMovieRent(int id, DateTimeOffset returnDate, CancellationToken cancellationToken);
    }
}