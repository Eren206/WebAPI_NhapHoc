using Microsoft.EntityFrameworkCore;
using testKetNoi.Data;
using testKetNoi.Dto;
using testKetNoi.Interfaces;
using testKetNoi.Models;

namespace testKetNoi.Repository
{
    public class SinhVienRepository : ISinhVienRepository
    {
        private readonly DataContext context;

        public SinhVienRepository(DataContext context)
        {
            this.context = context;
        }

        

        public SinhVien GetSinhVienByCCCD(string cccd)
        {
            return context.SinhVien.Where(s => s.SoCCCD == cccd).FirstOrDefault();
        }


        public ICollection<SinhVien> GetSinhViens()
        {
            return context.SinhVien.OrderBy(s => s.MaNganh).ToList();
        }
        public bool SinhVienExists(string cccd)
        {
            return context.SinhVien.Any(s => s.SoCCCD == cccd);
        }
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool UpdateSinhVien(SinhVien sinhVien)
        {
            context.SinhVien.Update(sinhVien);
            
            return Save();
        }

        public ICollection<HoSo> GetHoSos()
        {
            return context.HoSo.OrderBy(h => h.MaHoSo).ToList();
        }
    }
}
