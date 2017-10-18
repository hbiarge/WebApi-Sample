using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using Microsoft.Owin.Security.Authorization.WebApi;

namespace Api.Infrastructure.Authorization
{
    public class CustomResourceAuthorizeAttribute : ResourceAuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException(nameof(actionContext));
            }

            var isAuthenticated = actionContext.RequestContext?.Principal?.Identity?.IsAuthenticated;

            if (isAuthenticated.HasValue && isAuthenticated.Value == true)
            {
                actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(
                    HttpStatusCode.Forbidden,
                    "Forbidden");
            }
            else
            {
                actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(
                    HttpStatusCode.Unauthorized,
                    "Unauthorized");
            }
        }
    }
}