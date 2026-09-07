using ApiGateway.Application.DTOs;
using ApiGateway.Application.DTOs.QLBB;

namespace ApiGateway.Application.Interfaces;

public interface IBienBanKPHRepository
{
    Task<List<BienBanKPHDto>> GetAllAsync(
        CancellationToken cancellationToken = default);
}