namespace CinemaApp.Data.Configuration
{
    using CinemaApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserTicketConfiguration : IEntityTypeConfiguration<UserTicket>
    {
        public void Configure(EntityTypeBuilder<UserTicket> builder)
        {
            builder.HasKey(ut => new { ut.UserId, ut.TicketId });
            
            builder.HasOne(ut => ut.User)
                .WithMany()
                .HasForeignKey(ut => ut.UserId);

            builder.HasOne(ut => ut.Ticket)
                .WithMany()
                .HasForeignKey(ut => ut.TicketId);
        }
    }
}
