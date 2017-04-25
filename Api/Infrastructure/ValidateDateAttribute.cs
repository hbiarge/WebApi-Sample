using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Api.Infrastructure
{
    public class ValidateDateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var routeData = actionContext.RequestContext.RouteData;
            var year = int.Parse(routeData.Values["year"].ToString());
            var month = int.Parse(routeData.Values["month"].ToString());
            var day = int.Parse(routeData.Values["day"].ToString());

            if (DateBuilder.TryBuildFrom(year, month, day, out DateTime date) == false)
            {
                actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    "Invalid date");
            }
        }
    }
}