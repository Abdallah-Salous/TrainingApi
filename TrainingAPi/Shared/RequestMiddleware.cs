using Microsoft.AspNetCore.Mvc;

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
                _logger.LogError(ve.Message);
                var response = new ObjectResult("Invalid operation") { StatusCode = 400 };
                var actionContext = new ActionContext() { HttpContext = context };

                await response.ExecuteResultAsync(actionContext).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.StackTrace);
                var response = new ObjectResult("Internal server error") { StatusCode = 500 };
                var actionContext = new ActionContext() { HttpContext = context };

                await response.ExecuteResultAsync(actionContext).ConfigureAwait(false);
            }

        }

    }
}
