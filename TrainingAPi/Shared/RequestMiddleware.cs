
namespace TrainingAPi.Shared
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger("Training Api Logger");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogInformation("Request Execution Start");
                await _next(context).ConfigureAwait(false);
            }
            catch (TrainingValidationException ve)
            {
                _logger.LogError(ve ,ve.Message);

                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { message = "Invalid operation" });
            }
            catch (TrainingBadRequestException be)
            {
                _logger.LogError(be, be.Message);

                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync($"{be.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.StackTrace);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { message = "Internal server error" });
            }

        }

    }
}
