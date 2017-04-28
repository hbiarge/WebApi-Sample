using Domain.Aggregates.Cinemas;
using Domain.Aggregates.Films;
using Domain.Aggregates.Sessions;

namespace Api.IntegrationTests.Infrastructure.CollectionFixtures
{
    public class SeedData
    {
        public Cinema Cinema { get; set; }

        public Film[] Films { get; set; }

        public Session[] Sessions { get; set; }
    }
}