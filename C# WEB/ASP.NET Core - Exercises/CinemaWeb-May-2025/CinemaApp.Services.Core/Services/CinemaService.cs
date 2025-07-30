namespace CinemaApp.Services.Core.Services
{
    using CinemaApp.Data.Repository;
    using CinemaApp.Data.Repository.Interfaces;
    using CinemaApp.Services.Core.Interfaces;
    using CinemaApp.Web.ViewModels.Cinema;
    using CinemaApp.Web.ViewModels.Movie;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualBasic;

    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;

        public CinemaService(ICinemaRepository cinemaRepository)
        {
            _cinemaRepository = cinemaRepository;
        }

        public Task AddAsync(CinemaIndexViewModel model) => throw new NotImplementedException();
        public Task EditAsync(string id, CinemaIndexViewModel model) => throw new NotImplementedException();
        public async Task<IEnumerable<CinemaIndexViewModel>> GetAllCinemasAsync()
        {
            return await _cinemaRepository.GetAllAttached()
                .Where(c => !c.IsDeleted)
                .AsNoTracking()
                .Select(c => new CinemaIndexViewModel
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Location = c.Location,
                    HasMovies = c.CinemaMovies.Any(m => !m.IsDeleted)
                })
                .ToListAsync();
        }

        public Task<CinemaIndexViewModel> GetCinemaByIdAsync(string id) => throw new NotImplementedException();
        public Task<CinemaIndexViewModel?> GetForEditByIdAsync(string? id) => throw new NotImplementedException();
        public async Task HardDeleteAsync(string id)
        {
            var cinema = await _cinemaRepository.GetAllAttached()
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (cinema != null)
            {
                _cinemaRepository.Delete(cinema);
                await _cinemaRepository.SaveChangesAsync();
            }
        }

        public async Task SoftDeleteAsync(string id)
        {
            var cinema = await _cinemaRepository.GetAllAttached()
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (cinema != null && !cinema.IsDeleted)
            {
                cinema.IsDeleted = true;
                await _cinemaRepository.SaveChangesAsync();
            }
        }
    }
}
