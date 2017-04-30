using System;
using System.Linq;
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
    public class CreateSession
    {
        private readonly DatabaseFixture _fixture;

        private readonly CreateSessionBindingModel _model;

        public CreateSession(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _model = new CreateSessionBindingModel
            {
                FilmId = _fixture.SeedData.Films.First().Id,
                ScreenId = _fixture.SeedData.Cinema.Screens.First().Id,
                Start = DateTime.Today.AddDays(1).AddHours(17)
            };
        }

        [Fact]
        public async Task CreateSession_With_Valid_Data_Should_Return_Created()
        {
            var endpoint = $"api/v1/cinemas/{_fixture.SeedData.Cinema.Id}/sessions";
            var response = await _fixture.Server.CreateRequest(endpoint)
                .WithIdentity(Identities.User)
                .WithJsonContent(_model)
                .PostAsync();

            await response.IsSuccessStatusCodeOrTrow();

            response.Headers.Location.Should().NotBeNull();

            var values = await response.Content.ReadAsAsync<SessionViewModel>();

            values.Should().NotBeNull();
        }
    }
}