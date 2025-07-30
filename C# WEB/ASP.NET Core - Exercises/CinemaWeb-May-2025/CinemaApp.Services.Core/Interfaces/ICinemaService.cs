namespace CinemaApp.Services.Core.Interfaces
{
    using CinemaApp.Web.ViewModels.Cinema;

    public interface ICinemaService
    {
        Task<IEnumerable<CinemaIndexViewModel>> GetAllCinemasAsync();

        Task AddAsync(CinemaIndexViewModel model);

        Task<CinemaIndexViewModel> GetCinemaByIdAsync(string id);

        Task<CinemaIndexViewModel?> GetForEditByIdAsync(string? id);

        Task EditAsync(string id, CinemaIndexViewModel model);

        Task SoftDeleteAsync(string id);

        Task HardDeleteAsync(string id);
    }
}
