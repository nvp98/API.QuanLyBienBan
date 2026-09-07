using ApiGateway.API.Authentication;
using ApiGateway.Application.DTOs.QLBB;
using ApiGateway.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.API.Controllers;

[ApiController]
[Route("api/bbkph")]
[Authorize(AuthenticationSchemes = BasicAuthenticationOptions.DefaultScheme)]
public class BBKPHController : ControllerBase
{
    private readonly IBBKPHService _service;

    public BBKPHController(IBBKPHService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<BienBanKPHDto>>> GetAll(
        [FromQuery] DateTime? tuNgay,
        [FromQuery] DateTime? denNgay,
        [FromQuery] string? maNT,
        [FromQuery] int? pageSize,
        CancellationToken cancellationToken)
    {
        var data = await _service.GetAllAsync(
            tuNgay,
            denNgay,
            maNT,
            pageSize,
            cancellationToken);

        return Ok(data);
    }
}