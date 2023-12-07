using testKetNoi.Models;
using testKetNoi.Dto;
namespace testKetNoi.Interfaces
{
    public interface ISinhVienRepository
    {
        ICollection<SinhVien> GetSinhViens() ;
        SinhVien GetSinhVienByCCCD(string cccd);
        bool SinhVienExists(string cccd);
        bool isPay(string cccd);
        bool UpdateSinhVien(SinhVien sinhVien);
        ICollection<HoSo> GetHoSos();
        bool createHoSoSinhVien(HoSoSinhVien hssv);
        bool isHoSoRegister(HoSoSinhVien hssv);
        bool updateHoSoSinhVien(HoSoSinhVien hssv);
        bool isResNganHang(string cccd);
        bool Save();
    }
}
