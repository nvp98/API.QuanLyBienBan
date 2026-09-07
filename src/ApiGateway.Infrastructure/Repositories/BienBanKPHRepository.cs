using ApiGateway.Application.DTOs;
using ApiGateway.Application.DTOs.QLBB;
using ApiGateway.Application.Interfaces;
using ApiGateway.Infrastructure.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApiGateway.Infrastructure.Repositories;

public class BienBanKPHRepository : IBienBanKPHRepository
{
    private readonly GatewayDbContext _context;
    private readonly string _fileScanBaseUrl;

    public BienBanKPHRepository(
        GatewayDbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _fileScanBaseUrl = configuration["FileStorage:BbkphBaseUrl"] ?? string.Empty;
    }


    public async Task<List<BienBanKPHDto>> GetAllAsync(
        DateTime? tuNgay = null,
        DateTime? denNgay = null,
        string? maNT = null,
        int? pageSize = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.BienBanKPH
            .AsNoTracking()
            .AsQueryable();

        // Từ ngày
        if (tuNgay.HasValue)
        {
            query = query.Where(x =>
                x.NgayLap >= tuNgay.Value);
        }

        // Đến ngày
        if (denNgay.HasValue)
        {
            query = query.Where(x =>
                x.NgayLap <= denNgay.Value);
        }

        // Mã nhà thầu
        if (!string.IsNullOrWhiteSpace(maNT))
        {
            query = query.Where(x =>
                x.MaNT == maNT);
        }

        // pageSize không truyền hoặc <= 0 => mặc định 20
        var size = pageSize.HasValue && pageSize.Value > 0
            ? pageSize.Value
            : 20;

        var result = await query
            .OrderByDescending(x => x.NgayLap)
            .ThenByDescending(x => x.MaBB)
            .Take(size)
            .Select(x => new BienBanKPHDto
            {
                BBKPHID = x.BBKPHID,
                MaBB = x.MaBB,
                SoBienBan = x.SoBienBan,
                NgayLap = x.NgayLap,
                MaPB = x.MaPB,
                MaNT = x.MaNT,
                MoTa = x.MoTa,
                SoLuong = x.SoLuong,
                BienPhap = x.BienPhap,
                TrachNhiem = x.TrachNhiem,
                NguyenNhan = x.NguyenNhan,
                TrangThai = x.TrangThai,
                NgayTao = x.NgayTao,
                TenDangNhap = x.TenDangNhap,
                FileScan = x.FileScan,
                IsLock = x.IsLock,
                MaPBN = x.MaPBN
            })
            .ToListAsync(cancellationToken);

        foreach (var item in result)
        {
            if (!string.IsNullOrWhiteSpace(item.FileScan))
            {
                item.FileScan = _fileScanBaseUrl + Uri.EscapeDataString(item.FileScan);
            }
        }

        return result;
    }
}