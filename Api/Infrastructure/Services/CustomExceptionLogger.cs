using Serilog;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Api.Infrastructure.Services
{
    public class CustomExceptionLogger : IExceptionLogger
    {
        public Task LogAsync(
            ExceptionLoggerContext context,
            CancellationToken cancellationToken)
        {
            Log.Error(context.Exception, "Error processing the request");

            return Task.FromResult<object>(null);
        }
    }
}
