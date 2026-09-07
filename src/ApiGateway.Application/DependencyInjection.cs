using ApiGateway.Application.Interfaces;
using ApiGateway.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGatewayService, GatewayService>();
        services.AddScoped<IBBKPHService, BBKPHService>();
        return services;
    }
}
