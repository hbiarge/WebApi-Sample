using System.Security.Claims;
using System.Web.Http;
using Api;
using Api.Infrastructure.Authorization;
using Autofac;
using Autofac.Integration.WebApi;
using Infrastructure;
using Microsoft.Owin.Security.Authorization.Infrastructure;
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

            ApiConfiguration(app);

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

            // Add policy based authorization
            api.UseAuthorization(Policies.Configure);

            // Configure common options
            Api.ApiConfiguration.Configure(config);

            // Configure middlewares pipeline

            // ### Temporal authentication middleware
            api.Use(async (context, next) =>
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role, "Administrator"), 
                    new Claim(ClaimTypes.Role, "Vendor"), 
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
