using testKetNoi.Models;

namespace testKetNoi.Interfaces
{
    public interface IKTXRepository
    {
        IEnumerable<KTX> getAll();
        KTX GetKTX(string maKTX);
        bool KTXExists(string maKTX);
        bool dangKy(DangKyKTX dangKyKTX);
        bool isDangKy(string cccd,string maPhong);
        bool Save();
    }
}
