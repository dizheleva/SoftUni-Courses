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

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            builder.HasQueryFilter(c => !c.IsDeleted);

            // Define a composite index on Name and Location to ensure unique combinations only
            builder.HasIndex(c => new { c.Name, c.Location })
                .IsUnique(true);
        }
    }
}
