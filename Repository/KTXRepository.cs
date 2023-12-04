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

        public ICollection<KTX> GetKTXs()
        {
            return context.KTX.OrderBy(k => k.MaPhong).ToList();
        }

        public bool KTXExists(string maKTX)
        {
            return context.KTX.Any(k => k.MaPhong == maKTX);
        }
    }
}
