namespace CinemaApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore;
    using static CinemaApp.Data.Common.EntityConstants.MovieConstants;

    [Comment("Movie in the system")]
    public class Movie
    {
        [Comment("Movie identifier")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("Movie title")]
        [Required(ErrorMessage = "Title is required!")]
        [StringLength(TitleMaxLength, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = null!;

        [Comment("Movie genre")]
        [Required(ErrorMessage = "Genre is required!")]
        [StringLength(GenreMaxLength, ErrorMessage = "Genre cannot exceed 50 characters.")]
        public string Genre { get; set; } = null!;

        [Comment("Movie release date")]
        [Required(ErrorMessage = "Release date is required!")]
        public DateTime ReleaseDate { get; set; }

        [Comment("Movie director")]
        [Required(ErrorMessage = "Director is required!")]
        [StringLength(DirectorMaxLength, ErrorMessage = "Director cannot exceed 100 characters.")]
        public string Director { get; set; } = null!;

        [Comment("Movie duration in minutes")]
        [Range(DurationMin, DurationMax, ErrorMessage = "Duration must be between 1 and 300 minutes.")]
        public int Duration { get; set; }

        [Comment("Movie description")]
        [Required(ErrorMessage = "Description is required!")]
        [StringLength(DescriptionMaxLength, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; } = null!;

        [Comment("Movie image URL")]
        public string? ImageUrl { get; set; }

        [Comment("Indicates if the movie is deleted")]
        public bool IsDeleted { get; set; } = false;

        [Comment("Collection of cinemas showing this movie")]
        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; } = new HashSet<CinemaMovie>();

        [Comment("Collection of tickets associated with this movie")]
        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
