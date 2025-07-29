namespace CinemaApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore;

    [Comment("Cinema in the system")]
    public class Cinema
    {
        [Comment("Cinema identifier")]
        [Key]
        public Guid Id { get; set; }

        [Comment("Cinema name")]
        //[Required(ErrorMessage = NameRequiredMessage)]
        //[StringLength(NameMaxLength, ErrorMessage = NameMaxLengthMessage)]
        public string Name { get; set; } = null!;

        [Comment("Cinema name")]
        //[Required(ErrorMessage = LocationRequiredMessage)]
        //[StringLength(NameMaxLength, ErrorMessage = LocationMaxLengthMessage)]
        public string Location { get; set; } = null!;

        [Comment("Indicates if the cinema is deleted")]
        public bool IsDeleted { get; set; }

        // Navigation properties

        [Comment("Foreign key to the manager of the cinema")]
        public Guid? ManagerId { get; set; }
        [Comment("Manager of the cinema")]
        public virtual Manager? Manager { get; set; }

        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; } = new HashSet<CinemaMovie>();

    }
}
