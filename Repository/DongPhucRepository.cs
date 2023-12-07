using testKetNoi.Data;
using testKetNoi.Dto;
using testKetNoi.Interfaces;
using testKetNoi.Models;
namespace testKetNoi.Repository
{
    public class DongPhucRepository : IDongPhucRepository
    {
        private readonly DataContext context;
        public DongPhucRepository(DataContext context)
        {
            this.context = context;
        }
        public bool daMuaDongPhuc(MuaDongPhuc muaDongPhuc)
        {
            return context.MuaDongPhuc.Any(m => m.MaDongPhuc == muaDongPhuc.MaDongPhuc && m.SoCCCD == muaDongPhuc.SoCCCD);
        }

        public bool muaDongPhuc(MuaDongPhuc muaDongPhuc)
        {
            context.MuaDongPhuc.Add(muaDongPhuc);
            return Save();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool chinhSuaDongPhuc(MuaDongPhuc muaDongPhuc)
        {
            context.MuaDongPhuc.Update(muaDongPhuc);
            return Save();
        }

        public IEnumerable<DongPhuc> getAll()
        {
            return context.DongPhuc.OrderBy(d => d.TenDongPhuc).ToList();
        }
    }
}
