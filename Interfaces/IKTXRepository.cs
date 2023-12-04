using testKetNoi.Models;

namespace testKetNoi.Interfaces
{
    public interface IKTXRepository
    {
        ICollection<KTX> GetKTXs();
        KTX GetKTX(string maKTX);
        bool KTXExists(string maKTX);
    }
}
