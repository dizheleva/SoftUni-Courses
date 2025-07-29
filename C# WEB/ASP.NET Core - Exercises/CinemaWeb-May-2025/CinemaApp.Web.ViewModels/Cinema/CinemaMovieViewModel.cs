namespace CinemaApp.Web.ViewModels.Cinema
{
    public class CinemaMovieViewModel
    {
        public string MovieId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string PosterUrl { get; set; } = null!;

        public int Duration { get; set; }

        public bool IsIncluded { get; set; }

    }
}