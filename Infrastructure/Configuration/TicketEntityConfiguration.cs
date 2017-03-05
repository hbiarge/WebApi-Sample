using System.Data.Entity.ModelConfiguration;
using Domain.Aggregates.Sessions;

namespace Infrastructure.Configuration
{
    internal class TicketEntityConfiguration : EntityTypeConfiguration<Ticket>
    {
        public TicketEntityConfiguration()
        {
            ToTable("Tickets", "session");

            HasKey(x => new { x.SessionId, x.SeatId });

            Property(x => x.Price)
                .IsRequired();

            HasRequired(x => x.SessionSeat);
        }
    }
}