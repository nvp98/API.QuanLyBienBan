
using ApiGateway.Application.DTOs.NapLieuLoCaoDTO;
using ApiGateway.Application.Interfaces;
using ApiGateway.Infrastructure.Data.DBContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Infrastructure.Repositories;

public class DuLieuNapLieuLoCaoRepository : IDuLieuNapLieuLoCaoRepository
{
    private readonly ProductFormDbContext _context;

    public DuLieuNapLieuLoCaoRepository(ProductFormDbContext context)
    {
        _context = context;
    }

    public async Task<List<DuLieuNapLieuLoCaoDto>> TongHopDuLieuNapLieuLoCaoTheoNVLAsync(
        DateTime? tuNgay = null,
        DateTime? denNgay = null,
        int? idCa = null,
        int? idLoCao = null,
        CancellationToken cancellationToken = default)
    {
        var pTuNgay = new SqlParameter("@TuNgay", System.Data.SqlDbType.Date) { Value = (object?)tuNgay?.Date ?? DBNull.Value };
        var pDenNgay = new SqlParameter("@DenNgay", System.Data.SqlDbType.Date) { Value = (object?)denNgay?.Date ?? DBNull.Value };
        var pIdCa = new SqlParameter("@IDCa", System.Data.SqlDbType.Int) { Value = (object?)idCa ?? DBNull.Value };
        var pIdLoCao = new SqlParameter("@IDLoCao", System.Data.SqlDbType.Int) { Value = (object?)idLoCao ?? DBNull.Value };

        return await _context.Database
            .SqlQueryRaw<DuLieuNapLieuLoCaoDto>(
                "EXEC dbo.SP_API_LG2_NL_TongHopQuyKhoTheoNVL @TuNgay, @DenNgay, @IDCa, @IDLoCao",
                pTuNgay, pDenNgay, pIdCa, pIdLoCao)
            .ToListAsync(cancellationToken);
    }
}
