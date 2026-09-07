using ApiGateway.Application.DTOs;
using ApiGateway.Application.DTOs.QLBB;
using ApiGateway.Application.Interfaces;

namespace ApiGateway.Application.Services;

public class BBKPHService : IBBKPHService
{
    private readonly IBienBanKPHRepository _bienBanKPHRepository;

    public BBKPHService(
        IBienBanKPHRepository bienBanKPHRepository)
    {
        _bienBanKPHRepository = bienBanKPHRepository;
    }

    public async Task<List<BienBanKPHDto>> GetAllAsync(
      DateTime? tuNgay = null,
      DateTime? denNgay = null,
      string? maNT = null,
      int? pageSize = null,
      CancellationToken cancellationToken = default)
    {
        return await _bienBanKPHRepository.GetAllAsync(
            tuNgay,
            denNgay,
            maNT,
            pageSize,
            cancellationToken);
    }
}