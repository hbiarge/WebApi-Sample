using System.Data.Entity.ModelConfiguration;
using Domain.Aggregates.Cinemas;
using Domain.Aggregates.Films;

namespace Infrastructure.Configuration
{
    internal class FilmEntityConfiguration : EntityTypeConfiguration<Film>
    {
        public FilmEntityConfiguration()
        {
            ToTable("Films", "cine");

            HasKey(x => x.Id);

            Property(x => x.Title)
                .HasMaxLength(256)
                .IsRequired();

            Property(x => x.DurationInMinutes)
                .IsRequired();

            HasMany(x => x.Cinemas)
                .WithMany();
        }
    }
}