namespace CinemaApp.Data.Repository.Interfaces
{
    using System;
    using CinemaApp.Data.Models;
    public interface IMovieRepository : IRepository<Movie, Guid>
    {
    }
}
