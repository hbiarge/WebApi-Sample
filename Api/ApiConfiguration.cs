using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api
{
    public static class ApiConfiguration
    {
        public static void Configure(HttpConfiguration config)
        {
            // Map attribute routes
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new AuthorizeAttribute());

            // Show errors to local requests
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;
            
            // Remove Xml serializer
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Json serialization settings
            ConfigureJsonSerialization(config.Formatters.JsonFormatter.SerializerSettings);
        }

        private static void ConfigureJsonSerialization(JsonSerializerSettings serializerSettings)
        {
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            serializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
        }
    }
}
