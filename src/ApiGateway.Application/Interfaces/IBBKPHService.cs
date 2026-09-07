using ApiGateway.Application.DTOs.QLBB;

namespace ApiGateway.Application.Interfaces;

public interface IBBKPHService
{
    Task<List<BienBanKPHDto>> GetAllAsync(
      DateTime? tuNgay = null,
      DateTime? denNgay = null,
      string? maNT = null,
      int? pageSize = null,
      CancellationToken cancellationToken = default);
}