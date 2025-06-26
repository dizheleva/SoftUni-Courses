namespace CinemaApp.Services.Core.Interfaces
{
    using CinemaApp.Web.ViewModels.Movie;

    public interface IMovieService
    {
        Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync();

        Task AddAsync(MovieFormViewModel model);

        Task<MovieDetailsViewModel> GetMovieByIdAsync(string id);

        Task<MovieFormViewModel?> GetForEditByIdAsync(string? id);

        Task EditAsync(string id, MovieFormViewModel model);

        Task SoftDeleteAsync(string id);

        Task HardDeleteAsync(string id);
    }
}
