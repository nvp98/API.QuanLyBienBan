namespace ApiGateway.Application.DTOs.QLBB;

public class BienBanKPHDto
{
    public int BBKPHID { get; set; }

    public string MaBB { get; set; } = null!;

    public string? SoBienBan { get; set; }

    public DateTime? NgayLap { get; set; }

    public string? MaPB { get; set; }

    public string? MaNT { get; set; }

    public string? MoTa { get; set; }

    public string? SoLuong { get; set; }

    public string? BienPhap { get; set; }

    public string? TrachNhiem { get; set; }

    public string? NguyenNhan { get; set; }

    public int? TrangThai { get; set; }

    public DateTime? NgayTao { get; set; }

    public string? TenDangNhap { get; set; }

    public string? FileScan { get; set; }

    public bool? IsLock { get; set; }

    public string? MaPBN { get; set; }
}