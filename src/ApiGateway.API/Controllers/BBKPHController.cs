using ApiGateway.Application.DTOs.QLBB;
using ApiGateway.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.API.Controllers;

[ApiController]
[Route("api/bbkph")]
public class BBKPHController : ControllerBase
{
    private readonly IBBKPHService _service;

    public BBKPHController(IBBKPHService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<BienBanKPHDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var data = await _service.GetAllAsync(
            cancellationToken);

        return Ok(data);
    }
}