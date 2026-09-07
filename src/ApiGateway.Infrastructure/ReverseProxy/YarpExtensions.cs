using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Infrastructure.ReverseProxy;

public static class YarpExtensions
{
    public static IServiceCollection AddGatewayReverseProxy(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddReverseProxy()
            .LoadFromConfig(configuration.GetSection("ReverseProxy"));

        return services;
    }
}
