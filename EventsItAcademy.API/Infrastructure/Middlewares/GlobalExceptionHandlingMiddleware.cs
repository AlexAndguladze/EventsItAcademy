using Newtonsoft.Json;
using ToDo.API.Infrastructure.Models;

namespace EventsItAcademy.API.Infrastructure.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        public readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            var error = new ApiError(context, ex);
            var result = JsonConvert.SerializeObject(error);

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.Status.Value;

            await context.Response.WriteAsync(result);
            LogException(result);
        }

        private void LogException(string error)
        {
            var logInfo = $"{Environment.NewLine}---- Exception Log ----{Environment.NewLine}" + error;
            _logger.LogError(logInfo);
        }
    }
}
