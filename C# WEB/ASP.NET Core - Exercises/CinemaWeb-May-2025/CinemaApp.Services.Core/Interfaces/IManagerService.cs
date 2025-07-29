namespace CinemaApp.Services.Core.Interfaces
{
    public interface IManagerService 
    {
        Task<bool> ExistsByIdAsync(string? id);

        Task<bool> ExistsByUserIdAsync(string? userId);
    }
}
