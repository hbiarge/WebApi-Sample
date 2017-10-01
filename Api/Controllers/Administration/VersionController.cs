using System.Web.Http;
using Api.Infrastructure.Authorization;
using Api.Infrastructure.Versioning;
using Microsoft.Owin.Security.Authorization.WebApi;
using Microsoft.Web.Http;

namespace Api.Controllers.Administration
{
    [Version1]
    [Version2]
    [RoutePrefix("api/v{version:apiVersion}/version")]
    [ResourceAuthorize(Policy = Policies.Administrator)]
    public class VersionController : ApiController
    {
        [Route]
        public string Get() => "v1.0";

        [Route, MapToApiVersion("2.0")]
        public string GetV2() => "v2.0";
    }
}
