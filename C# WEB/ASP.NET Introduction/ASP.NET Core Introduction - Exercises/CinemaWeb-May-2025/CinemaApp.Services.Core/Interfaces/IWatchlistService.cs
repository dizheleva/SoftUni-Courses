namespace CinemaApp.Services.Core.Interfaces
{
    using CinemaApp.Web.ViewModels.Watchlist;

    public interface IWatchlistService
    {
        Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId);
    }
}
