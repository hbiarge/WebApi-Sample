using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var builder = new ContainerBuilder();
            AutofacConfiguration.Configure(builder);
            builder.RegisterModule<InfrastructureModule>();

            var container = builder.Build();

            var config = new HttpConfiguration
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(container)
            };
            ApiConfiguration.Configure(config);

            // Overriden configurations
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);

            app.UseTestServerAuthentication();
            app.UseWebApi(config);
        }
    }
}
