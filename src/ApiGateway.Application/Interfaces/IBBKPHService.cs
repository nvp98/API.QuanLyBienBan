using ApiGateway.Application.DTOs.QLBB;

namespace ApiGateway.Application.Interfaces;

public interface IBBKPHService
{
    Task<List<BienBanKPHDto>> GetAllAsync(
        CancellationToken cancellationToken = default);
}