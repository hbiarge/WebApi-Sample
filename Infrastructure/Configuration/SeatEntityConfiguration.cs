using System.Data.Entity.ModelConfiguration;
using Domain.Aggregates.Cinemas;

namespace Infrastructure.Configuration
{
    internal class SeatEntityConfiguration : EntityTypeConfiguration<Seat>
    {
        public SeatEntityConfiguration()
        {
            ToTable("Seats", "cine");

            HasKey(x => x.Id);

            Property(x => x.Row)
                .IsRequired();

            Property(x => x.Number)
                .IsRequired();

            HasMany(x => x.Sessions)
                .WithRequired(x => x.Seat)
                .WillCascadeOnDelete(false);
        }
    }
}