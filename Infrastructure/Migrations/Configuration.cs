using Domain.Aggregates.Cinemas;
using Domain.Aggregates.Fims;

namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Infrastructure.DatabaseContext context)
        {
            var conAir = new Film("ConAir", 120);
            var batman = new Film("Batman", 95);

            context.Films.AddOrUpdate(c => c.Title, conAir, batman);

            var cinema = new Cinema("Palafox");
            var screen1 = cinema.CreateScreen(name: "1", rows: 5, seatsPerRow: 5);
            var screen2 = cinema.CreateScreen(name: "2", rows: 6, seatsPerRow: 6);
            var screen3 = cinema.CreateScreen(name: "3", rows: 7, seatsPerRow: 7);
            cinema.AddFilm(batman);

            context.Cinemas.AddOrUpdate(c => c.Name, cinema);

            var sesion1 = cinema.CreateSession(screen1, batman, new DateTime(2017, 3, 5, 18, 0, 0));
            var sesion2 = cinema.CreateSession(screen2, batman, new DateTime(2017, 3, 5, 19, 45, 0));
            var sesion3 = cinema.CreateSession(screen3, batman, new DateTime(2017, 3, 5, 22, 0, 0));
            context.Sessions.AddRange(new[] { sesion1, sesion2, sesion3 });
        }
    }
}
