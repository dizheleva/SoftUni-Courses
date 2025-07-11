namespace CinemaApp.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class Ticket
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Price { get; set; }

        // Navigation properties
        public Guid MovieId { get; set; }
        public virtual Movie Movie { get; set; } = null!;
        public Guid CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;
    }
}
