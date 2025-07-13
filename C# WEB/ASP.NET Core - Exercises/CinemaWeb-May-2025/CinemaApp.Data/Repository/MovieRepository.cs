namespace CinemaApp.Data.Repository
{
    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;

    public class MovieRepository : BaseRepository<Movie, Guid>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
