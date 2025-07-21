using Serilog.Context;

namespace TrainingAPi.Shared
{
    public class SerilogEnricherMiddleware
    {
        private readonly RequestDelegate _next;

        public SerilogEnricherMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString();

            // Push ClientIp into the log context for this request
            using (LogContext.PushProperty("ClientIp", clientIp ?? "unknown"))
            {
                await _next(context);
            }
        }
    }

}
