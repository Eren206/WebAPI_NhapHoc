using testKetNoi.Data;
using testKetNoi.Interfaces;
using testKetNoi.Models;

namespace testKetNoi.Repository
{
    public class KTXRepository : IKTXRepository
    {
        private readonly DataContext context;

        public KTXRepository(DataContext context)
        {
            this.context = context;
        }
        public KTX GetKTX(string maKTX)
        {
            return context.KTX.Where(k => k.MaPhong == maKTX).FirstOrDefault();
        }

        public IEnumerable<KTX> getAll()
        {
            return context.KTX.OrderBy(k => k.TenPhong).ToList();
        }

        public bool KTXExists(string maKTX)
        {
            return context.KTX.Any(k => k.MaPhong == maKTX);
        }

        public bool dangKy(DangKyKTX dangKyKTX)
        {
            context.DangKyKTX.Add(dangKyKTX);
            return Save();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool isDuplicate(string cccd,string maPhong)
        {
            return context.DangKyKTX.Any(k => k.SoCCCD == cccd && k.MaPhong == maPhong);
        }

        public bool isRes(string cccd)
        {
            return context.DangKyKTX.Any(k => k.SoCCCD == cccd);
        }

        public bool updateKTX(DangKyKTX dangKyKTX)
        {
            context.DangKyKTX.Update(dangKyKTX);
            return Save();
        }
    }
}
