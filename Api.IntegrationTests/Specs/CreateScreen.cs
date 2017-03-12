using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Acheve.Owin.Testing.Security;
using Api.BindingModels;
using Api.IntegrationTests.Infrastructure;
using Api.IntegrationTests.Infrastructure.CollectionFixtures;
using Aplication.Commands;
using Aplication.Queries.ViewModels;
using FluentAssertions;
using Xunit;

namespace Api.IntegrationTests.Specs
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
                Name = "12",
                Rows = 2,
                SeatsPerRow = 2
            };
        }

        [Fact]
        public async Task CreateScreen_With_Valid_Data_Should_Return_Created()
        {
            var endpoint = $"cinemas/{_fixture.SeedData.Cinema.Id}/screens";
            var response = await _fixture.Server.CreateRequest(endpoint)
                .WithIdentity(Identities.User)
                .And(x => x.Content = new ObjectContent(_model.GetType(), _model, new JsonMediaTypeFormatter()))
                .PostAsync();

            await response.IsSuccessStatusCodeOrTrow();

            response.Headers.Location.Should().NotBeNull();

            var values = await response.Content.ReadAsAsync<CreateScreenResponse>();

            values.Should().NotBeNull();
        }
    }
}