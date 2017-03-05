using System.Diagnostics;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace Aplication.Pipeline
{
    public class TimingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next)
        {
            var clock = Stopwatch.StartNew();
            var response = await next();
            clock.Stop();
            Log.Information("Handler executed in {elapsedms} ms", clock.ElapsedMilliseconds);
            return response;
        }
    }
}