namespace CinemaApp.Data
{
    using System.Reflection;
    using CinemaApp.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<UserMovie> UserMovies { get; set; }
        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<CinemaMovie> CinemaMovies { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<UserTicket> UserTickets { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Apply configurations
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Additional configurations can be added here if needed
        }
    }
}
