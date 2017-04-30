using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using Microsoft.Web.Http.Description;
using Swashbuckle.Swagger;

namespace WebHost
{
    /// <summary>
    /// Represents the Swagger/Swashbuckle operation filter used to document the implicit API version parameter.
    /// </summary>
    public class ImplicitApiVersionParameter : IOperationFilter
    {
        /// <inheriteddoc />
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var description = apiDescription as VersionedApiDescription;

            // if the api explorer did not capture an API version for this operation
            // then the action must be API version-neutral; there's nothing to add
            if (description?.ApiVersion == null)
            {
                return;
            }

            var parameters = operation.parameters;

            if (parameters == null)
            {
                operation.parameters = parameters = new List<Parameter>();
            }

            // note: in most applications, service authors will choose a single, consistent
            // approach to how API versioning is applied. this sample uses a:
            //
            // 1. query string parameter method with the name "api-version"
            // 2. url path segement with the route parameter name "api-version"
            //
            // unless you allow multiple API versioning methods in your application,
            // your implementation should be even simpler.

            // consider the url path segment parameter first
            var parameter = parameters.SingleOrDefault(p => p.name == "api-version");

            if (parameter == null)
            {
                // the only other method in this sample is by query string
                parameter = new Parameter()
                {
                    name = "api-version",
                    required = true,
                    @in = "query",
                    type = "string"
                };

                parameters.Add(parameter);
            }

            // update the default value with the current API version so that
            // the route can be invoked in the "Try It!" feature
            parameter.@default = description.ApiVersion.ToString();
            parameter.description = "The requested API version";
        }
    }
}