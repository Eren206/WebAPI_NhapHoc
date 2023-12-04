using testKetNoi.Dto;
using testKetNoi.Models;

namespace testKetNoi.Interfaces
{
    public interface IHocPhiRepository
    {
        decimal getTongHocPhi(string cccd);
        ChiTietHocPhiDto getChiTietHocPhi(string cccd);
        bool SaveBill(HoaDon hoaDon,string cccd);
        public bool Save();
    }
}
