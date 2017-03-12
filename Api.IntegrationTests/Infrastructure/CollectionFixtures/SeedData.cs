using Domain.Aggregates.Cinemas;

namespace Api.IntegrationTests.Infrastructure.CollectionFixtures
{
    public class SeedData
    {
        public SeedData(Cinema cinema)
        {
            Cinema = cinema;
        }

        public Cinema Cinema { get; }
    }
}