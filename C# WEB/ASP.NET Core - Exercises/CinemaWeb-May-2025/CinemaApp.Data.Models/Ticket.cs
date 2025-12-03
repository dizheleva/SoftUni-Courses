namespace CinemaApp.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    [Comment("Ticket in the system")]
    public class Ticket
    {
        [Comment("Ticket identifier")]
        public Guid Id { get; set; }

        [Comment("Ticket price")]
        public decimal Price { get; set; }

        [Comment("Foreign key to the Movie projection in a Cinema")]
        public Guid CinemaMovieId { get; set; }

        // Navigation property for the ticket's projection in a cinema
        public virtual CinemaMovie CinemaMovie { get; set; } = null!;

        [Comment("Foreign key to the owner of the ticket")]
        public string UserId { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
