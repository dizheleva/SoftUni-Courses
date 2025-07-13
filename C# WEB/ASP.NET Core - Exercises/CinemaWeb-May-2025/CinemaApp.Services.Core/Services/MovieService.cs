namespace CinemaApp.Services.Core.Services
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using CinemaApp.Services.Core.Interfaces;
    using CinemaApp.Web.ViewModels.Movie;
    using Microsoft.EntityFrameworkCore;
    using static CinemaApp.Data.Common.EntityConstants.MovieConstants;

    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task HardDeleteAsync(string id)
        {
            var movie = await _movieRepository.GetAllAttached()
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (movie != null)
            {
                _movieRepository.Delete(movie);
                await _movieRepository.SaveChangesAsync();
            }
        }

        public async Task SoftDeleteAsync(string id)
        {
            var movie = await _movieRepository.GetAllAttached()
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (movie != null && !movie.IsDeleted)
            {
                movie.IsDeleted = true;
                await _movieRepository.SaveChangesAsync();
            }
        }

        public async Task EditAsync(string id, MovieFormViewModel model)
        {
            var movie = await _movieRepository.GetAllAttached()
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (movie == null)
            {
                return;
            }

            movie.Title = model.Title;
            movie.Genre = model.Genre;
            movie.Director = model.Director;
            movie.Description = model.Description;
            movie.Duration = model.Duration;
            movie.ReleaseDate = DateTime.ParseExact(model.ReleaseDate, DateFormat, CultureInfo.InvariantCulture);
            movie.ImageUrl = model.ImageUrl;

            await _movieRepository.SaveChangesAsync();
        }


        public async Task<MovieFormViewModel?> GetForEditByIdAsync(string id)
        {
            return await _movieRepository.GetAllAttached()
                .Where(m => m.Id.ToString() == id)
                .Select(m => new MovieFormViewModel
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Genre = m.Genre,
                    Director = m.Director,
                    Description = m.Description,
                    Duration = m.Duration,
                    ReleaseDate = m.ReleaseDate.ToString(DateFormat),
                    ImageUrl = m.ImageUrl
                })
                .FirstOrDefaultAsync();
        }


        public async Task<MovieDetailsViewModel> GetMovieByIdAsync(string id)
        {
            var movie = await _movieRepository.GetAllAttached()
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id.ToString() == id && !m.IsDeleted);

            if (movie == null)
            {
                return null;
            }

            return new MovieDetailsViewModel
            {
                Id = movie.Id.ToString(),
                Title = movie.Title,
                Genre = movie.Genre,
                Director = movie.Director,
                Description = movie.Description,
                Duration = movie.Duration,
                ReleaseDate = movie.ReleaseDate.ToString(DateFormat),
                ImageUrl = movie.ImageUrl
            };
        }


        public async Task AddAsync(MovieFormViewModel model)
        {
            var movie = new Movie
            {
                Title = model.Title,
                Genre = model.Genre,
                Director = model.Director,
                Description = model.Description,
                Duration = model.Duration,
                ReleaseDate = DateTime.ParseExact(model.ReleaseDate, DateFormat, CultureInfo.InvariantCulture),
                ImageUrl = model.ImageUrl
            };

            await _movieRepository.AddAsync(movie);
            await _movieRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync()
        {
            return await _movieRepository.GetAllAttached()
                .Where(m => !m.IsDeleted)
                .AsNoTracking()
                .Select(m => new AllMoviesIndexViewModel
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Genre = m.Genre,
                    Director = m.Director,
                    ReleaseDate = m.ReleaseDate.ToString(DateFormat),
                    ImageUrl = m.ImageUrl
                })
                .ToListAsync();
        }

    }
}
