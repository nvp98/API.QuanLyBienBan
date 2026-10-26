using ApiGateway.Application.DTOs.NapLieuLoCaoDTO;
namespace ApiGateway.Application.Interfaces;

public interface IDuLieuNapLieuLoCaoRepository
{
    Task<List<DuLieuNapLieuLoCaoDto>> TongHopDuLieuNapLieuLoCaoTheoNVLAsync(
        DateTime? tuNgay = null,
        DateTime? denNgay = null,
        int? idCa = null,
        int? idLoCao = null,
        CancellationToken cancellationToken = default);
}
