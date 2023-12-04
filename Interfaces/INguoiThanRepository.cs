using testKetNoi.Models;

namespace testKetNoi.Interfaces
{
    public interface INguoiThanRepository
    {
        NguoiThan GetNguoiThanById(int id);
        int CreateNguoiThan(NguoiThan nguoiThan);
        bool UpdateNguoiThan(NguoiThan nguoiThan);
        Tuple<int,int> getIDNguoiThans(string cccd);
        bool createThongTinNguoiThan(int IdNT1, int IdNT2, string cccd);
        bool IsCreated(string cccd);
    }
}
