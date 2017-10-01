using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Domain.Aggregates.Cinemas;
using Domain.Aggregates.Films;
using Domain.Aggregates.Sessions;

namespace Infrastructure.Initializers
{
    public static class Initiaizer
    {
        public static void Seed(DatabaseContext context)
        {
            var conAir = new Film("ConAir", 120);
            var batman = new Film("Batman", 95);
            var pulpFiction = new Film("Pulp fiction", 100);

            context.Films.AddOrUpdate(c => c.Title, conAir, batman, pulpFiction);

            var cinema = new Cinema("Palafox");
            var screen1 = cinema.CreateScreen(name: "Aneto", rows: 5, seatsPerRow: 5);
            var screen2 = cinema.CreateScreen(name: "Monte Perdido", rows: 6, seatsPerRow: 6);
            var screen3 = cinema.CreateScreen(name: "Posets", rows: 7, seatsPerRow: 7);
            batman.Cinemas.Add(cinema);

            context.Cinemas.AddOrUpdate(c => c.Name, cinema);

            if (context.Sessions.Any() == false)
            {
                var sesion1 = Session.Create(screen1, conAir, new DateTime(2017, 3, 5, 18, 0, 0));
                sesion1.Publish();
                var sesion2 = Session.Create(screen2, batman, new DateTime(2017, 3, 5, 19, 45, 0));
                var sesion3 = Session.Create(screen3, pulpFiction, new DateTime(2017, 3, 5, 22, 0, 0));

                context.Sessions.AddRange(new[] { sesion1, sesion2, sesion3 });
            }
        }
    }
}
