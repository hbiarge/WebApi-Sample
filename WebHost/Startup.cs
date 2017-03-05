using System.Security.Claims;
using System.Web.Http;
using Api;
using Autofac;
using Autofac.Integration.WebApi;
using Infrastructure;
using Microsoft.Owin;
using Owin;
using Serilog;

[assembly: OwinStartup(typeof(WebHost.Startup))]

namespace WebHost
{
    public class Startup
    {
        private IContainer _container;

        public void Configuration(IAppBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                //.WriteTo.LiterateConsole()
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .CreateLogger();
            
            var builder = new ContainerBuilder();
            AutofacConfiguration.Configure(builder);
            builder.RegisterModule<InfrastructureModule>();

            _container = builder.Build();

            app.UseAutofacMiddleware(_container);

            app.Map("/api", ApiConfiguration);

            app.UseWelcomePage();
        }

        private void ApiConfiguration(IAppBuilder api)
        {
            var config = new HttpConfiguration
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(_container)
            };

            Api.ApiConfiguration.Configure(config);
            
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

            api.UseAutofacWebApi(config);
            api.UseWebApi(config);
        }
    }
}
