namespace CinemaApp.Data.Configuration
{
    using CinemaApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static CinemaApp.Data.Common.EntityConstants.CinemaConstants;

    public class CinemaMovieConfiguration : IEntityTypeConfiguration<CinemaMovie>
    {
        public void Configure(EntityTypeBuilder<CinemaMovie> builder)
        {
            builder.HasKey(cm => cm.Id);

            // Define pseudo-composite key
            builder
                .HasIndex(cm => new { cm.CinemaId, cm.MovieId })
                .IsUnique();

            builder
                .HasOne(cm => cm.Cinema)
                .WithMany(c => c.CinemaMovies)
                .HasForeignKey(cm => cm.CinemaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(cm => cm.Movie)
                .WithMany(m => m.CinemaMovies)
                .HasForeignKey(cm => cm.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(cm => cm.AvailableTickets)
                .IsRequired()
                .HasDefaultValue(0);
                

            builder
                .Property(cm => cm.IsDeleted)
                .HasDefaultValue(false);

            builder
                .Property(cm => cm.Showtimes)
                .IsRequired()
                .HasColumnType(ShowtimeColumnType)
                .HasDefaultValue(ShowtimeDefaultValue);

            builder
                .HasQueryFilter(cm => !cm.IsDeleted &&
                                                cm.Movie.IsDeleted == false &&
                                                cm.Cinema.IsDeleted == false);
        }
    }
}
