using testKetNoi.Models;

namespace testKetNoi.Interfaces
{
    public interface IDongPhucRepository
    {
        IEnumerable<DongPhuc> getAll();
        bool muaDongPhuc(MuaDongPhuc muaDongPhuc);
        bool daMuaDongPhuc(MuaDongPhuc muaDongPhuc);
        bool chinhSuaDongPhuc(MuaDongPhuc muaDongPhuc);
        bool Save();
    }
}
