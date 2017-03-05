using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using FluentValidation;
using Newtonsoft.Json;

namespace Api.Infrastructure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
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

    public class ValidationErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception as ValidationException;

            if (exception == null)
            {
                return;
            }

            var errors = exception.Errors
                .Select(x => new { x.PropertyName, x.ErrorMessage })
                .ToArray();
            var message = JsonConvert.SerializeObject(errors);

            actionExecutedContext.Request.CreateErrorResponse(
                HttpStatusCode.BadRequest,
                message);
        }
    }
}
