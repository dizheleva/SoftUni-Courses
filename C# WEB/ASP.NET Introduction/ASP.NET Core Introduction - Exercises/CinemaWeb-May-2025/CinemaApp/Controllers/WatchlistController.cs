namespace CinemaApp.Web.Controllers
{
    using CinemaApp.Services.Core.Interfaces;
    using CinemaApp.Web.ViewModels.Watchlist;
    using Microsoft.AspNetCore.Mvc;

    public class WatchlistController : BaseController
    {
        private readonly IWatchlistService _watchlistService;

        public WatchlistController(IWatchlistService watchlistService)
        {
            _watchlistService = watchlistService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Index", "Home");
            }

            var userId = GetUserId();
            var model = await _watchlistService.GetUserWatchlistAsync(userId);

            return View(model);
        }

        //Add a movie to the watchlist
        [HttpPost]
        public async Task<IActionResult> Add(string movieId)
        {
            if (!IsUserAuthenticated())
            {                
                return RedirectToAction("Index", "Home");
            }

            var userId = GetUserId();

            bool isInWatchlist = await _watchlistService.IsMovieInWatchlistAsync(userId, Guid.Parse(movieId));

            if (!isInWatchlist)
            {
                await _watchlistService.AddToWatchlistAsync(userId, movieId);
            }

            return RedirectToAction(nameof(Index));
        }

        //Remove a movie from the watchlist
        [HttpPost]
        public IActionResult Remove(string movieId)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            var userId = GetUserId();
            var movieGuid = Guid.Parse(movieId);
            bool isInWatchlist = _watchlistService.IsMovieInWatchlistAsync(userId, movieGuid).Result;

            if (isInWatchlist)
            {
                _watchlistService.RemoveFromWatchlistAsync(userId, movieId).Wait();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
