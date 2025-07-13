namespace CinemaApp.Data.Repository.Interfaces
{
    using CinemaApp.Data.Models;

    public interface IWatchlistRepository : IRepository<UserMovie, string>
    {
        Task<UserMovie?> GetByCompositeKeyAsync(string userId, string movieId);

        Task<bool> ExistsAsync(string userId, string movieId);
    }
}
