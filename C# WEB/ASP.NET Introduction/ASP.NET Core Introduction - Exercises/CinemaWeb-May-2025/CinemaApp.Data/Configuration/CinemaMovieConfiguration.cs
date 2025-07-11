namespace CinemaApp.Data.Configuration
{
    using CinemaApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CinemaMovieConfiguration : IEntityTypeConfiguration<CinemaMovie>
    {
        public void Configure(EntityTypeBuilder<CinemaMovie> builder)
        {
            builder.HasKey(cm => new { cm.CinemaId, cm.MovieId });
            
            builder.HasOne(cm => cm.Cinema)
                .WithMany(c => c.CinemaMovies)
                .HasForeignKey(cm => cm.CinemaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cm => cm.Movie)
                .WithMany(m => m.CinemaMovies)
                .HasForeignKey(cm => cm.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(cm => cm.AvailableTickets)
                .IsRequired();

            builder.Property(cm => cm.IsDeleted)
                .HasDefaultValue(false);

            // Values can be written in entity constants
            builder.Property(cm => cm.Showtimes)
                .HasColumnType("varchar(5)")
                .HasDefaultValue("00000");

            // builder.HasQueryFilter(cm => !cm.IsDeleted);
        }
    }
}
