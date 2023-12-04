using testKetNoi.Models;

namespace testKetNoi.Interfaces
{
    public interface IAuthenRepository
    {
        public string generateToken(TaiKhoan taiKhoan);
        public TaiKhoan getValidUser(string soCccd,string matKhau);
    }
}
