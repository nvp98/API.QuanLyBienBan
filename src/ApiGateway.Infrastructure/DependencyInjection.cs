using ApiGateway.Application.Interfaces;
using ApiGateway.Infrastructure.Data.DBContext;
using ApiGateway.Infrastructure.Repositories;
using ApiGateway.Infrastructure.ReverseProxy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<GatewayDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("QLBienBan2023"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()));

        services.AddDbContext<ProductFormDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("PRODUCT_FORM"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()));

        services.AddScoped<IBienBanKPHRepository, BienBanKPHRepository>();
        services.AddScoped<IDuLieuNapLieuLoCaoRepository, DuLieuNapLieuLoCaoRepository>();

        services.AddGatewayReverseProxy(configuration);
        return services;
    }
}
