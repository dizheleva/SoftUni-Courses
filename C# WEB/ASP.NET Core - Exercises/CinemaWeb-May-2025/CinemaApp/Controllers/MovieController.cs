namespace CinemaApp.Web.Controllers
{
    using CinemaApp.Services.Core.Interfaces;
    using CinemaApp.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _movieService.GetAllMoviesAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _movieService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);

            return movie == null ? NotFound() : View(movie);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await _movieService.GetForEditByIdAsync(id);
            
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, MovieFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _movieService.EditAsync(id, model);
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            return movie == null ? NotFound() : View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {            
            await _movieService.SoftDeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
