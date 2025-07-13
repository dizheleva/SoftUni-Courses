namespace CinemaApp.Data.Configuration
{
    using CinemaApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder
                .Property(t => t.UserId)
                .IsRequired(true);

            builder.HasOne(t => t.CinemaMovie)
                .WithMany(m => m.Tickets)
                .HasForeignKey(t => t.CinemaMovieId);

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);

            builder
                .HasIndex(t => new { t.CinemaMovieId, t.UserId })
                .IsUnique(true);

            builder
                .HasQueryFilter(t => t.CinemaMovie.IsDeleted == false);
        }
    }
}
