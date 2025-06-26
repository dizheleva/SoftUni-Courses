namespace CinemaApp.Services.Core.Services
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using CinemaApp.Data;
    using CinemaApp.Data.Models;
    using CinemaApp.Services.Core.Interfaces;
    using CinemaApp.Web.ViewModels.Movie;
    using Microsoft.EntityFrameworkCore;
    using static CinemaApp.Data.Common.EntityConstants.MovieConstants;

    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
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

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(string id, MovieFormViewModel model)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id.ToString() == id);

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

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync()
        {
            return await _context.Movies
                .Where(m => !m.IsDeleted)
                .AsNoTracking() //  improves performance when the results will only be read, not updated
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

        public async Task<MovieFormViewModel?> GetForEditByIdAsync(string? id)
        {
            bool isValidId = Guid.TryParse(id, out var movieId);
            
            return await _context.Movies
                .AsNoTracking()
                .Where(m => m.Id == movieId)
                .Select(m => new MovieFormViewModel()
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Genre = m.Genre,
                    ReleaseDate = m.ReleaseDate.ToString(DateFormat),
                    Director = m.Director,
                    Duration = m.Duration,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl
                })
                .FirstOrDefaultAsync();
        }

        public async Task<MovieDetailsViewModel?> GetMovieByIdAsync(string? id)
        {
            return await _context.Movies
                .AsNoTracking()
                .Where(m => m.Id.ToString() == id && !m.IsDeleted)
                .Select(m => new MovieDetailsViewModel
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Genre = m.Genre,
                    ReleaseDate = m.ReleaseDate.ToString(DateFormat),
                    Director = m.Director,
                    Duration = m.Duration,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl
                })
                .FirstOrDefaultAsync();
        }

        public async Task SoftDeleteAsync(string id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (movie != null)
            {
                movie.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task HardDeleteAsync(string id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
