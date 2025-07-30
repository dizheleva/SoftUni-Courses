namespace CinemaApp.Data.Repository
{
    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;

    public class CinemaRepository : BaseRepository<Cinema, Guid>, ICinemaRepository
    {
        public CinemaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
