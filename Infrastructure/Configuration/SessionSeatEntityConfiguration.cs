using System.Data.Entity.ModelConfiguration;
using Domain.Aggregates.Sessions;

namespace Infrastructure.Configuration
{
    internal class SessionSeatEntityConfiguration : EntityTypeConfiguration<SessionSeat>
    {
        public SessionSeatEntityConfiguration()
        {
            ToTable("SessionSeats", "session");

            HasKey(x => new { x.SessionId, x.SeatId });

            Property(x => x.Sold)
                .IsRequired();
        }
    }
}