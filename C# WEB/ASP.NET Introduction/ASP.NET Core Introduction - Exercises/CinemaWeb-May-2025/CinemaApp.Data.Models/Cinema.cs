namespace CinemaApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore;

    using static CinemaApp.Data.Common.EntityConstants.CinemaConstants;

    [Comment("Cinema in the system")]
    public class Cinema
    {
        [Comment("Cinema identifier")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("Cinema name")]
        [Required(ErrorMessage = NameRequiredMessage)]
        [StringLength(NameMaxLength, ErrorMessage = NameMaxLengthMessage)]
        public string Name { get; set; } = null!;

        [Comment("Cinema name")]
        [Required(ErrorMessage = LocationRequiredMessage)]
        [StringLength(NameMaxLength, ErrorMessage = LocationMaxLengthMessage)]
        public string Location { get; set; } = null!;

        [Comment("Indicates if the cinema is deleted")]
        public bool IsDeleted { get; set; } = false;
                       
        // Navigation properties
        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; } = new List<CinemaMovie>();

        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
