using ApiGateway.Application.DTOs;
using ApiGateway.Application.DTOs.QLBB;
using ApiGateway.Application.Interfaces;
using ApiGateway.Infrastructure.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Infrastructure.Repositories;

public class BienBanKPHRepository : IBienBanKPHRepository
{
    private readonly GatewayDbContext _context;

    public BienBanKPHRepository(
        GatewayDbContext context)
    {
        _context = context;
    }

    public async Task<List<BienBanKPHDto>> GetAllAsync(
    CancellationToken cancellationToken = default)
    {
        return await _context.BienBanKPH
            .AsNoTracking()
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
    }
}