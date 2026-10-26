using ApiGateway.Application.DTOs.NapLieuLoCaoDTO;
using ApiGateway.Application.Interfaces;

namespace ApiGateway.Application.Services;

public class DuLieuNapLieuLoCaoService : IDuLieuNapLieuLoCaoService
{
    private readonly IDuLieuNapLieuLoCaoRepository _repository;

    public DuLieuNapLieuLoCaoService(IDuLieuNapLieuLoCaoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<DuLieuNapLieuLoCaoDto>> TongHopDuLieuNapLieuLoCaoTheoNVLAsync(
        DateTime? tuNgay = null,
        DateTime? denNgay = null,
        int? idCa = null,
        int? idLoCao = null,
        CancellationToken cancellationToken = default)
    {
        return await _repository.TongHopDuLieuNapLieuLoCaoTheoNVLAsync(
            tuNgay,
            denNgay,
            idCa,
            idLoCao,
            cancellationToken);
    }
}
