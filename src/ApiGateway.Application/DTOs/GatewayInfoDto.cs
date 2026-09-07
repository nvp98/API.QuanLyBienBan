namespace ApiGateway.Application.DTOs;

public sealed record GatewayInfoDto(
    string Service,
    string Version,
    string Framework,
    string ReverseProxy,
    string Status,
    DateTimeOffset ServerTime
);
