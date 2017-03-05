using System.Threading.Tasks;
using MediatR;
using Serilog;
using Serilog.Context;

namespace Aplication.Pipeline
{
    public class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next)
        {
            using (LogContext.PushProperty("MediatRRequestType", typeof(TRequest).FullName))
            {
                Log.Information("Start executing handler");
                var response = await next();
                Log.Information("End executing handler");
                return response;
            }
        }
    }
}
