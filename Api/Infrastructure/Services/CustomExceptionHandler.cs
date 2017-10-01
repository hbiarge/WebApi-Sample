using System.Web.Http.ExceptionHandling;

namespace Api.Infrastructure.Services
{
    public class CustomExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            base.Handle(context);
        }
    }
}
