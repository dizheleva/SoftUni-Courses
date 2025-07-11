namespace CinemaApp.Services.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using CinemaApp.Services.Core.Interfaces;
    using CinemaApp.Web.ViewModels.Watchlist;
    using Microsoft.EntityFrameworkCore;
    using static CinemaApp.Data.Common.EntityConstants;

    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository _watchlistRepository; 

        public WatchlistService(IWatchlistRepository watchlistRepository)
        {
            _watchlistRepository = watchlistRepository;
        }

        public async Task RemoveFromWatchlistAsync(string userId, string movieId)
        {
            var userMovie = await _watchlistRepository.GetByCompositeKeyAsync(userId, movieId);

            if (userMovie != null)
            {
                _watchlistRepository.Delete(userMovie);
                await _watchlistRepository.SaveChangesAsync();
            }
        }


        public async Task AddToWatchlistAsync(string userId, string movieId)
        {
            var userMovie = new UserMovie
            {
                UserId = userId,
                MovieId = Guid.Parse(movieId)
            };
            await _watchlistRepository.AddAsync(userMovie);
            await _watchlistRepository.SaveChangesAsync();
        }

        public async Task<bool> IsMovieInWatchlistAsync(string userId, Guid movieId)
        {
            return await _watchlistRepository.ExistsAsync(userId, movieId.ToString());
        }

        public async Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId)
        {
            return await _watchlistRepository.GetAllAttached()
                .Where(um => um.UserId == userId)
                .Select(um => new WatchlistViewModel
                {
                    MovieId = um.Movie.Id.ToString(),
                    Title = um.Movie.Title,
                    Genre = um.Movie.Genre,
                    ImageUrl = um.Movie.ImageUrl,
                    ReleaseDate = um.Movie.ReleaseDate.ToString(ReleaseDateFormat)
                }).ToListAsync();
        }
    }
}
