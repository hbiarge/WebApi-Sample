using System.Security.Claims;
using System.Web.Http;
using Api;
using Autofac;
using Autofac.Integration.WebApi;
using Infrastructure;
using Owin;

namespace ConsoleHost
{
    public class Startup
    {
        private IContainer _container;

        public void Configuration(IAppBuilder app)
        {
            _container = ConfigureAndBuildContainer();
            
            app.UseAutofacMiddleware(_container);
            app.DisposeScopeOnAppDisposing(_container);

            app.Map("/api", ApiConfiguration);

            app.UseWelcomePage();
        }

        private static IContainer ConfigureAndBuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ApiAutofacModule
            {
                SampleConfiguration = true
            });
            builder.RegisterModule<InfrastructureAutofacModule>();

            return builder.Build();
        }

        private void ApiConfiguration(IAppBuilder api)
        {
            // Create HttpConfiguration
            var config = new HttpConfiguration
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(_container)
            };

            // Configure common options
            Api.ApiConfiguration.Configure(config);

            // Configure middlewares pipeline

            // ### Temporal authentication middleware
            api.Use(async (context, next) =>
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "Hugo"),
                },
                "Custom");
                context.Authentication.User = new ClaimsPrincipal(identity);
                await next();
            });
            // ###

            api.UseAutofacWebApi(config);
            api.UseWebApi(config);
        }
    }
}
