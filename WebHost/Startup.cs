using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
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

            // Configure common options
            Api.ApiConfiguration.Configure(config);

            var apiExplorer = config.AddVersionedApiExplorer();

            config.EnableSwagger(
                "{apiVersion}/swagger",
                swagger =>
                {
                    // build a swagger document and endpoint for each discovered API version
                    swagger.MultipleApiVersions(
                        (apiDescription, version) =>
                            apiDescription.GetGroupName() == version,
                            info =>
                            {
                                foreach (var group in apiExplorer.ApiDescriptions)
                                {
                                    var apiVersion = group.ApiVersion;
                                    var description = "Cinematic api.";

                                    if (group.IsDeprecated)
                                    {
                                        description += " This API version has been deprecated.";
                                    }

                                    info.Version(apiExplorer.GetGroupName(apiVersion), $"Cinematic API {apiVersion}")
                                        .Contact(c => c.Name("Hugo Biarge").Email("hbiarge@painconcepts.com"))
                                        .Description(description)
                                        .License(l => l.Name("MIT").Url("https://opensource.org/licenses/MIT"))
                                        .TermsOfService("Shareware");
                                }
                            });


                    // add a custom operation filter which documents the implicit API version parameter
                    // swagger.OperationFilter<ImplicitApiVersionParameter>();

                    // integrate xml comments
                    // swagger.IncludeXmlComments(XmlCommentsFilePath);
                })
                .EnableSwaggerUi();

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

        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = System.AppDomain.CurrentDomain.RelativeSearchPath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }
    }
}
