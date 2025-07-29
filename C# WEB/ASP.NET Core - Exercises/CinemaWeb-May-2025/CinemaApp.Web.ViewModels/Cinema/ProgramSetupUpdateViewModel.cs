namespace CinemaApp.Web.ViewModels.Cinema
{
    public class ProgramSetupUpdateViewModel
    {
        public string CinemaId { get; set; } = null!;

        public IEnumerable<CinemaMovieViewModel> Movies { get; set; } = new List<CinemaMovieViewModel>();
    }
}
