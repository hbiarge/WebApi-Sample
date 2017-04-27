using System.Data.Entity.ModelConfiguration;
using Domain.Aggregates.Cinemas;

namespace Infrastructure.Configuration
{
    internal class CinemaEntityConfiguration : EntityTypeConfiguration<Cinema>
    {
        public CinemaEntityConfiguration()
        {
            ToTable("Cinemas", "cine");

            HasKey(x => x.Id);

            Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            HasMany(x => x.Screens)
                .WithRequired(x => x.Cinema);            
        }
    }
}
