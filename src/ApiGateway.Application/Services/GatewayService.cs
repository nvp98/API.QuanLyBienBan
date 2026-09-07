using ApiGateway.Application.DTOs;
using ApiGateway.Application.Interfaces;

namespace ApiGateway.Application.Services;

public sealed class GatewayService : IGatewayService
{
    public GatewayInfoDto GetInfo()
    {
        return new GatewayInfoDto(
            Service: "API Gateway",
            Version: "1.0.0",
            Framework: ".NET 9",
            ReverseProxy: "YARP",
            Status: "Running",
            ServerTime: DateTimeOffset.Now
        );
    }
}
