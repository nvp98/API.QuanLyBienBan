using ApiGateway.Application.DTOs;

namespace ApiGateway.Application.Interfaces;

public interface IGatewayService
{
    GatewayInfoDto GetInfo();
}
