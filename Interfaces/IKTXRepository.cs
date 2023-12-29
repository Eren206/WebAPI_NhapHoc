using testKetNoi.Models;

namespace testKetNoi.Interfaces
{
    public interface IKTXRepository
    {
        IEnumerable<KTX> getAll();
        KTX GetKTX(string maKTX);
        bool KTXExists(string maKTX);
        bool dangKy(DangKyKTX dangKyKTX);
        bool isDuplicate(string cccd,string maPhong);
        bool isRes(string cccd);
        bool updateKTX(DangKyKTX dangKyKTX);
        bool Save();
    }
}
