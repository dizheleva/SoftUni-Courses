namespace CinemaApp.Services.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CinemaApp.Data;
    using CinemaApp.Services.Core.Interfaces;
    using CinemaApp.Web.ViewModels.Watchlist;

    public class WatchlistService : IWatchlistService
    {
        private readonly ApplicationDbContext _context; 

        public WatchlistService(ApplicationDbContext context)
        {
            _context = context;
        }   

        public Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId) => throw new NotImplementedException();
    }
}
