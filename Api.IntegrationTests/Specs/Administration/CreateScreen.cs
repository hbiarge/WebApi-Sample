using System.Net.Http;
using System.Threading.Tasks;
using Api.BindingModels;
using Api.IntegrationTests.Infrastructure;
using Api.IntegrationTests.Infrastructure.CollectionFixtures;
using Aplication.Queries.ViewModels;
using FluentAssertions;
using Microsoft.Owin.Testing;
using Xunit;

namespace Api.IntegrationTests.Specs.Administration
{
    [Collection(Collections.Database)]
    public class CreateScreen
    {
        private readonly DatabaseFixture _fixture;

        private readonly CreateScreenBindingModel _model;

        public CreateScreen(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _model = new CreateScreenBindingModel
            {
                Name = "Tendeñera",
                Rows = 2,
                SeatsPerRow = 2
            };
        }

        [Fact]
        public async Task CreateScreen_With_Valid_Data_Should_Return_Created()
        {
            var endpoint = $"api/v1/cinemas/{_fixture.SeedData.Cinema.Id}/screens";
            var response = await _fixture.Server.CreateRequest(endpoint)
                .WithIdentity(Identities.User)
                .WithJsonContent(_model)
                .PostAsync();

            await response.IsSuccessStatusCodeOrTrow();

            response.Headers.Location.Should().NotBeNull();

            var values = await response.Content.ReadAsAsync<ScreenViewModel>();

            values.Should().NotBeNull();
        }
    }
}