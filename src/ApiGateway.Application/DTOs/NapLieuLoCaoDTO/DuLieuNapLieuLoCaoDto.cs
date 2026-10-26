namespace ApiGateway.Application.DTOs.NapLieuLoCaoDTO;

public class DuLieuNapLieuLoCaoDto
{
    public DateTime? Ngay { get; set; }
    public int? IDCa { get; set; }
    public int IDLoCao { get; set; }
    public int IDNVL { get; set; }
    public string? TenNVL_NM { get; set; }
    public string? TenNVL_TK { get; set; }
    public decimal TongGiaTri { get; set; }
    public decimal DoAm { get; set; }
    public decimal TongQuyKho { get; set; }
}
