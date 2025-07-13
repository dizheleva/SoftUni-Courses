namespace CinemaApp.Data.Repository
{
    using System.Threading.Tasks;
    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class WatchlistRepository : BaseRepository<UserMovie, string>, IWatchlistRepository
    {
        private readonly ApplicationDbContext dbContext;

        public WatchlistRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ExistsAsync(string userId, string movieId)
        {
            return await this.GetAllAttached().AnyAsync(um => um.UserId == userId && um.MovieId.ToString() == movieId);
        }
        public async Task<UserMovie?> GetByCompositeKeyAsync(string userId, string movieId)
        {
            return await this.GetAllAttached()
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId.ToString() == movieId);
        }
    }
}
