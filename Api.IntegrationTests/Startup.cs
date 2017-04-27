using System.Web.Http;
using Acheve.Owin.Testing.Security;
using Autofac;
using Autofac.Integration.WebApi;
using Infrastructure;
using Owin;

namespace Api.IntegrationTests
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = BuildContainer();
            var config = BuidHttpConfiguration(container);

            // Configure OWIN pipeline
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);

            app.UseTestServerAuthentication();
            app.UseWebApi(config);
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ApiAutofacModule
            {
                SampleConfiguration = true
            });
            builder.RegisterModule<InfrastructureAutofacModule>();

            return builder.Build();
        }

        private static HttpConfiguration BuidHttpConfiguration(IContainer container)
        {
            // Create HttpConfiguration
            var config = new HttpConfiguration
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(container)
            };

            // Configure common options
            ApiConfiguration.Configure(config);

            // Overriden configurations
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            return config;
        }
    }
}
