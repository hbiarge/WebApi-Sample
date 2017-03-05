using System.Data.Entity.ModelConfiguration;
using Domain.Aggregates.Sessions;

namespace Infrastructure.Configuration
{
    internal class SessionEntityConfiguration : EntityTypeConfiguration<Session>
    {
        public SessionEntityConfiguration()
        {
            ToTable("Sessions", "session");

            HasKey(x => x.Id);

            Property(x => x.Start)
                .IsRequired();

            HasRequired(x => x.Screen)
                .WithMany();

            HasRequired(x => x.Film)
                .WithMany();

            HasMany(x => x.Seats)
                .WithRequired(x => x.Session)
                .WillCascadeOnDelete(false);
        }
    }
}