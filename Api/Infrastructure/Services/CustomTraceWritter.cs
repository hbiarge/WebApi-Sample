using Serilog;
using System;
using System.Web.Http.Tracing;
using System.Net.Http;

namespace Api.Infrastructure.Services
{
    public class CustomTraceWritter : ITraceWriter
    {
        public void Trace(
            HttpRequestMessage request,
            string category,
            TraceLevel level,
            Action<TraceRecord> traceAction)
        {
            TraceRecord traceRecord = new TraceRecord(request, category, level);
            traceAction(traceRecord);
            WriteTrace(traceRecord);
        }

        protected void WriteTrace(TraceRecord traceRecord)
        {
            var message = string.Format(
                "{0};{1};{2}",
                traceRecord.Operator,
                traceRecord.Operation,
                traceRecord.Message);

            Log.Debug(message);
        }
    }
}
