using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Acheve.Owin.Testing.Security;
using Autofac;
using Autofac.Integration.WebApi;
using FluentAssertions;
using Microsoft.Owin.Testing;
using Owin;
using Xunit;

namespace Api.IntegrationTests
{
    public class GetVauesShoud
    {
        private readonly TestServer _testServer;

        public GetVauesShoud()
        {
            _testServer = TestServer.Create(app =>
            {
                var builder = new ContainerBuilder();
                AutofacConfiguration.Configure(builder);
                var container = builder.Build();

                var config = new HttpConfiguration
                {
                    DependencyResolver = new AutofacWebApiDependencyResolver(container)
                };
                ApiConfiguration.Configure(config);

                app.UseAutofacMiddleware(container);
                app.UseAutofacWebApi(config);

                app.UseTestServerAuthentication();
                app.UseWebApi(config);
            });
        }

        [Fact]
        public async Task ReturnValues()
        {
            var response = await _testServer.CreateRequest("values")
                .WithIdentity(Identities.User)
                .GetAsync();

            await response.IsSuccessStatusCodeOrTrow();

            var values = await response.Content.ReadAsAsync<string[]>();

            values.Should().HaveCount(2);
        }
    }
}
