using ApiGateway.API.Middleware;

namespace ApiGateway.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseGatewayMiddleware(
        this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<CorrelationIdMiddleware>();

        return app;
    }
}
