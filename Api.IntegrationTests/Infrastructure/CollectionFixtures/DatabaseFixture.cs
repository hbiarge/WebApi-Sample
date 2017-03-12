using System;
using System.Data.Entity;
using System.Linq;
using Infrastructure;
using Infrastructure.Initializers;
using Microsoft.Owin.Testing;

namespace Api.IntegrationTests.Infrastructure.CollectionFixtures
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways());

            using (var context = new DatabaseContext())
            {
                var firstCinema = context.Cinemas
                    .AsNoTracking()
                    .First();

                SeedData = new SeedData(firstCinema);
            }

            // Build the test server
            Server = TestServer.Create<Startup>();
        }

        public TestServer Server { get; }

        public SeedData SeedData { get; }

        public void Dispose()
        {
            Database.Delete("cinematic");

            Server.Dispose();
        }
    }
}
