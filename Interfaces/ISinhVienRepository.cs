using testKetNoi.Models;
using testKetNoi.Dto;
namespace testKetNoi.Interfaces
{
    public interface ISinhVienRepository
    {
        ICollection<SinhVien> GetSinhViens() ;
        SinhVien GetSinhVienByCCCD(string cccd);
        bool SinhVienExists(string cccd);
        bool UpdateSinhVien(SinhVien sinhVien);
        ICollection<HoSo> GetHoSos();
        bool CreateHoSoSinhVien();
        bool Save();
    }
}
