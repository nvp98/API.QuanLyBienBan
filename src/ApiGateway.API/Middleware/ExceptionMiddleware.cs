using System.Text.Json;

namespace ApiGateway.API.Middleware;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception. TraceId: {TraceId}", context.TraceIdentifier);

            if (context.Response.HasStarted)
                throw;

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                success = false,
                message = "Internal server error.",
                traceId = context.TraceIdentifier
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
