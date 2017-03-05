using System.Data.Entity.ModelConfiguration;
using Domain.Aggregates.Cinemas;

namespace Infrastructure.Configuration
{
    internal class ScreenEntityConfiguration : EntityTypeConfiguration<Screen>
    {
        public ScreenEntityConfiguration()
        {
            ToTable("Screens", "cine");

            HasKey(x => x.Id);

            Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            HasMany(x => x.Seats)
                .WithRequired(x => x.Screen);
        }
    }
}