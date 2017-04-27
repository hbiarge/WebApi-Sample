using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Domain.Aggregates.Cinemas;
using Domain.Aggregates.Films;

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

            var sesion1 = cinema.CreateSession(screen1, conAir, new DateTime(2017, 3, 5, 18, 0, 0));
            var sesion2 = cinema.CreateSession(screen2, batman, new DateTime(2017, 3, 5, 19, 45, 0));
            var sesion3 = cinema.CreateSession(screen3, pulpFiction, new DateTime(2017, 3, 5, 22, 0, 0));

            context.Sessions.AddRange(new[] { sesion1, sesion2, sesion3 });
        }
    }
}
