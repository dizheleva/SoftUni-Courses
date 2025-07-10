namespace CinemaApp.Services.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CinemaApp.Data;
    using CinemaApp.Data.Models;
    using CinemaApp.Services.Core.Interfaces;
    using CinemaApp.Web.ViewModels.Watchlist;
    using Microsoft.EntityFrameworkCore;
    using static CinemaApp.Data.Common.EntityConstants;

    public class WatchlistService : IWatchlistService
    {
        private readonly ApplicationDbContext _context; 

        public WatchlistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddToWatchlistAsync(string userId, string movieId)
        {
            var userMovie = new UserMovie
            {
                UserId = userId,
                MovieId = Guid.Parse(movieId)
            };
            await _context.UserMovies.AddAsync(userMovie);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId)
        {
            return await _context.UserMovies
                .Where(um => um.UserId == userId)
                .Select(um => new WatchlistViewModel
                {
                    MovieId = um.Movie.Id.ToString(),
                    Title = um.Movie.Title,
                    Genre = um.Movie.Genre,
                    ImageUrl = um.Movie.ImageUrl,
                    ReleaseDate = um.Movie.ReleaseDate.ToString(MovieConstants.DateFormat)
                })
                .ToListAsync();
        }
        public async Task<bool> IsMovieInWatchlistAsync(string userId, Guid movieId)
        {
            return await _context.UserMovies
                .AnyAsync(um => um.UserId == userId && um.MovieId == movieId);
        }
        public async Task RemoveFromWatchlistAsync(string userId, string movieId)
        {
            var userMovie = await _context.UserMovies
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == Guid.Parse(movieId));

            if (userMovie != null)
            {
                _context.UserMovies.Remove(userMovie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
