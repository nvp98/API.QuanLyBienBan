using ApiGateway.Application.DTOs;
using ApiGateway.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.API.Controllers;

[ApiController]
[Route("api/gateway")]
public sealed class GatewayController : ControllerBase
{
    private readonly IGatewayService _gatewayService;

    public GatewayController(IGatewayService gatewayService)
    {
        _gatewayService = gatewayService;
    }

    [HttpGet("info")]
    [ProducesResponseType(typeof(GatewayInfoDto), StatusCodes.Status200OK)]
    public IActionResult Info()
    {
        return Ok(_gatewayService.GetInfo());
    }
}
