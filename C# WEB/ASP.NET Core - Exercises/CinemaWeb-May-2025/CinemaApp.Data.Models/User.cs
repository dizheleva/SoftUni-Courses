namespace CinemaApp.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public virtual Manager? Manager { get; set; }

        public virtual ICollection<UserMovie> Watchlist { get; set; } = new HashSet<UserMovie>();

        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
