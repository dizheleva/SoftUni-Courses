namespace CinemaApp.Data
{
    using System.Reflection;
    using CinemaApp.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Movie> Movies { get; set; }

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
