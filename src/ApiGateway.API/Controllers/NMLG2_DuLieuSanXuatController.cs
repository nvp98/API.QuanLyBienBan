using ApiGateway.Application.DTOs.NapLieuLoCaoDTO;
using ApiGateway.API.Authentication;
using ApiGateway.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.API.Controllers;

[ApiController]
[Route("api/lg2/nl")]
[Authorize(AuthenticationSchemes = BasicAuthenticationOptions.DefaultScheme)]
public class NMLG2_DuLieuSanXuatController : ControllerBase
{
    private readonly IDuLieuNapLieuLoCaoService _service;

    public NMLG2_DuLieuSanXuatController(IDuLieuNapLieuLoCaoService service)
    {
        _service = service;
    }

    [HttpGet("du-lieu-nap-lieu-lo-cao-theo-nvl")]
    public async Task<ActionResult<List<DuLieuNapLieuLoCaoDto>>> TongHopDuLieuNapLieuLoCaoTheoNVL(
        [FromQuery] DateTime? tuNgay,
        [FromQuery] DateTime? denNgay,
        [FromQuery] int? idCa,
        [FromQuery] int? idLoCao,
        CancellationToken cancellationToken)
    {
        var data = await _service.TongHopDuLieuNapLieuLoCaoTheoNVLAsync(
            tuNgay,
            denNgay,
            idCa,
            idLoCao,
            "LG2",
            cancellationToken);

        return Ok(data);
    }
    [HttpGet("du-lieu-nap-lieu-lo-cao-theo-nvl-lg1")]
    public async Task<ActionResult<List<DuLieuNapLieuLoCaoDto>>> TongHopDuLieuNapLieuLoCaoTheoNVL_LG1(
    [FromQuery] DateTime? tuNgay,
    [FromQuery] DateTime? denNgay,
    [FromQuery] int? idCa,
    [FromQuery] int? idLoCao,
    CancellationToken cancellationToken)
    {
        // LG1: Lò cao 1, 2, 3, 4
        var data = await _service.TongHopDuLieuNapLieuLoCaoTheoNVLAsync(
            tuNgay,
            denNgay,
            idCa,
            idLoCao,
            "LG1",
            cancellationToken);

        return Ok(data);
    }
}
