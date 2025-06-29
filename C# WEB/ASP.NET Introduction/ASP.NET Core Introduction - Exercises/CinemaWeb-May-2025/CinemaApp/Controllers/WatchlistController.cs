namespace CinemaApp.Web.Controllers
{
    using CinemaApp.Web.ViewModels.Watchlist;
    using Microsoft.AspNetCore.Mvc;

    public class WatchlistController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new List<WatchlistViewModel>());
        }

        //Add a movie to the watchlist
        [HttpPost]
        public IActionResult Add(WatchlistViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Logic to add the movie to the watchlist
                // This could involve saving to a database or session
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }

        //Remove a movie from the watchlist
        [HttpPost]
        public IActionResult Remove(string movieId)
        {
            if (!string.IsNullOrEmpty(movieId))
            {
                // Logic to remove the movie from the watchlist
                // This could involve removing from a database or session
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
