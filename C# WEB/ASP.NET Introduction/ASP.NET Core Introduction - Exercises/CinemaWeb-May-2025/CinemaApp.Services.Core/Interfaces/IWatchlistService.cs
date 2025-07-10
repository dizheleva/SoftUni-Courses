namespace CinemaApp.Services.Core.Interfaces
{
    using CinemaApp.Web.ViewModels.Watchlist;

    public interface IWatchlistService
    {
        Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId);

        Task<bool> IsMovieInWatchlistAsync(string userId, Guid movieId);

        Task AddToWatchlistAsync(string userId, string movieId);

        Task RemoveFromWatchlistAsync(string userId, string movieId);
    }
}
