namespace CinemaApp.Data.Configuration
{
    using CinemaApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserMovieConfiguration : IEntityTypeConfiguration<UserMovie>
    {
        public void Configure(EntityTypeBuilder<UserMovie> builder)
        {
            builder.HasKey(um => new { um.UserId, um.MovieId });

            builder
               .Property(aum => aum.UserId)
               .IsRequired();

            // Define default value for soft-delete functionality
            builder
                .Property(aum => aum.IsDeleted)
                .HasDefaultValue(false);

            // Configure relation between ApplicationUserMovie and IdentityUser
            // The IdentityUser does not contain navigation property, as it is built-in type from the ASP.NET Core Identity
            builder
                .HasOne(aum => aum.User)
                .WithMany() // We do not have navigation property from the IdentityUser side
                .HasForeignKey(aum => aum.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relation between ApplicationUserMovie and Movie
            builder
                .HasOne(aum => aum.Movie)
                .WithMany(m => m.UserMovies)
                .HasForeignKey(aum => aum.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define query filter to hide the deleted entries in the user Watchlist
            builder
                .HasQueryFilter(aum => aum.IsDeleted == false &&
                                                        aum.Movie.IsDeleted == false);
        }
    }
}
