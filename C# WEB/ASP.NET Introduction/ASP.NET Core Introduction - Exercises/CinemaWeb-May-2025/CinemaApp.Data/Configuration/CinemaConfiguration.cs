namespace CinemaApp.Data.Configuration
{
    using CinemaApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static CinemaApp.Data.Common.EntityConstants.CinemaConstants;

    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder.Property(c => c.Location)
                .IsRequired()
                .HasMaxLength(LocationMaxLength);

            builder.HasMany(c => c.CinemaMovies)
                .WithOne(cm => cm.Cinema)
                .HasForeignKey(cm => cm.CinemaId);
            
            builder.HasMany(c => c.Tickets)
                .WithOne(t => t.Cinema)
                .HasForeignKey(t => t.CinemaId);

            // builder.HasQueryFilter(c => !c.IsDeleted);

            // Define a composite index on Name and Location
            builder.HasIndex(c => new { c.Name, c.Location })
                .IsUnique(true);
        }
    }
}
