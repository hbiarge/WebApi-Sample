using System.Security.Claims;
using System.Web.Http;
using Api;
using Autofac;
using Autofac.Integration.WebApi;
using Infrastructure;
using Microsoft.Owin;
using Owin;
using Serilog;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(WebHost.Startup))]

namespace WebHost
{
    public class Startup
    {
        private IContainer _container;

        public Startup()
        {
            Log.Logger = new LoggerConfiguration()
                //.WriteTo.LiterateConsole()
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .CreateLogger();
        }

        public void Configuration(IAppBuilder app)
        {
            _container = ConfigureAndBuildContainer();

            app.UseAutofacMiddleware(_container);
            app.DisposeScopeOnAppDisposing(_container);

            var config = new HttpConfiguration
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(_container)
            };

            config.EnableSwagger(c => c.SingleApiVersion("v1", "Cinematic API"))
                .EnableSwaggerUi();

            // Configure common options
            Api.ApiConfiguration.Configure(config);

            // Configure middlewares pipeline

            // ### Temporal authentication middleware
            app.Use(async (context, next) =>
            {
                var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, "Hugo"),
                    },
                    "Custom");
                context.Authentication.User = new ClaimsPrincipal(identity);
                await next();
            });
            // ####

            app.UseAutofacWebApi(config);
            app.UseWebApi(config);

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
    }
}
