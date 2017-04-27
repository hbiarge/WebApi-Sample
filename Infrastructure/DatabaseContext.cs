using System.Data.Entity;
using System.Threading.Tasks;
using Domain;
using Domain.Aggregates.Cinemas;
using Domain.Aggregates.Films;
using Domain.Aggregates.Sessions;
using Infrastructure.Configuration;

namespace Infrastructure
{
    public class DatabaseContext : DbContext, IUnitOfWork
    {
        public DatabaseContext()
            : base("Cinematic")
        {
        }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Film> Films { get; set; }

        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CinemaEntityConfiguration());
            modelBuilder.Configurations.Add(new ScreenEntityConfiguration());
            modelBuilder.Configurations.Add(new SeatEntityConfiguration());
            modelBuilder.Configurations.Add(new FilmEntityConfiguration());
            modelBuilder.Configurations.Add(new SessionEntityConfiguration());
            modelBuilder.Configurations.Add(new SessionSeatEntityConfiguration());
            modelBuilder.Configurations.Add(new TicketEntityConfiguration());
        }

        async Task IUnitOfWork.CommitAsync()
        {
            await SaveChangesAsync();
        }
    }
}
