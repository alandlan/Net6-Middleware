using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Middleware.API.Middleware
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                _logger.LogInformation("Request: {0}", context.Request.Path);
                
                await _next(context);
                _logger.LogInformation("Response: {0}", context.Response.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                ProblemDetails problemDetails = new ProblemDetails
                {
                    Title = "An error occurred",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message
                };

                var json = JsonSerializer.Serialize(problemDetails);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/problem+json";
                await context.Response.WriteAsync(json);
            }
        }

    }
}
