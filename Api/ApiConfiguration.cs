using System.Web.Http;
using Api.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using Api.Infrastructure.Services;
using Microsoft.Web.Http.Routing;

namespace Api
{
    public static class ApiConfiguration
    {
        public static void Configure(HttpConfiguration config)
        {
            // Map attribute routes
            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                {
                    ["apiVersion"] = typeof( ApiVersionRouteConstraint )
                }
            };

            config.AddApiVersioning(o => o.ReportApiVersions = true);
            config.MapHttpAttributeRoutes(constraintResolver);

            config.Filters.Add(new CustomAuthorizeAttribute());
            config.Filters.Add(new ValidationErrorAttribute());

            // Show errors to local requests
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            // Remove Xml serializer
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            CustomizeServices(config);

            ConfigureJsonSerialization(config.Formatters.JsonFormatter.SerializerSettings);
        }

        private static void CustomizeServices(HttpConfiguration config)
        {
            config.Services.Replace(typeof(IExceptionLogger), new CustomExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new CustomExceptionHandler());

            //config.Services.Replace(typeof(System.Web.Http.Tracing.ITraceWriter), new CustomTraceWritter());
        }

        private static void ConfigureJsonSerialization(JsonSerializerSettings serializerSettings)
        {
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            serializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}
