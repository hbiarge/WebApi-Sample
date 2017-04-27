using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using FluentValidation;
using Newtonsoft.Json;

namespace Api.Infrastructure
{
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