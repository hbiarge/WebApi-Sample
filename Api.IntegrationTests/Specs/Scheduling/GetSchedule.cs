using System.Threading.Tasks;
using Api.IntegrationTests.Infrastructure;
using Api.IntegrationTests.Infrastructure.CollectionFixtures;
using Microsoft.Owin.Testing;
using Xunit;

namespace Api.IntegrationTests.Specs.Scheduling
{
    [Collection(Collections.Database)]
    public class GetSchedule
    {
        private readonly DatabaseFixture _fixture;

        public GetSchedule(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetSchedule_Should_Return_Values()
        {
            var endpoint = $"cinemas/{_fixture.SeedData.Cinema.Id}/schedule/2017/2/29";
            var response = await _fixture.Server.CreateRequest(endpoint)
                .WithIdentity(Identities.User)
                .GetAsync();

            await response.IsSuccessStatusCodeOrTrow();

            //var values = await response.Content.ReadAsAsync<CinemaViewModel[]>();

            //values.Should().HaveCount(1);
        }
    }
}
