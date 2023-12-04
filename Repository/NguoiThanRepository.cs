
using Microsoft.EntityFrameworkCore;
using testKetNoi.Data;
using testKetNoi.Interfaces;
using testKetNoi.Models;

namespace testKetNoi.Repository
{
    public class NguoiThanRepository : INguoiThanRepository
    {
        private readonly DataContext context;
        public NguoiThanRepository(DataContext context)
        {
            this.context = context;
        }
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public int CreateNguoiThan(NguoiThan nguoiThan)
        {
            context.Add(nguoiThan);
            if (Save()){ 
                int newId = nguoiThan.IdNguoiThan;
                return newId; 
            }
            return -1;
        }
        
        public NguoiThan GetNguoiThanById(int id)
        {
            return context.NguoiThan.Where(nt => nt.IdNguoiThan == id).FirstOrDefault();
        }

        public bool UpdateNguoiThan(NguoiThan nguoiThan)
        {
            context.NguoiThan.Update(nguoiThan);
            return Save();
        }

        public Tuple<int,int> getIDNguoiThans(string cccd)
        {
            var id1= context.ThongTinNguoiThan.Where(t => t.SoCCCD == cccd).ToList();
            
            var a = Tuple.Create(id1[0].IdNguoiThan, id1[1].IdNguoiThan);

            return a; 
        }
        public bool IsCreated(string cccd)
        {
            if ((context.ThongTinNguoiThan.Count(t => t.SoCCCD == cccd)) == 2) return true;
            else return false;
        }
        public bool createThongTinNguoiThan(int IdNT1, int IdNT2, string cccd)
        {
            var NguoiThan1 = context.NguoiThan.Where(nt => nt.IdNguoiThan == IdNT1).FirstOrDefault();
            var NguoiThan2 = context.NguoiThan.Where(nt => nt.IdNguoiThan == IdNT2).FirstOrDefault();
            var sinhVien = context.SinhVien.Where(s => s.SoCCCD == cccd).FirstOrDefault();
            var ThongTinNguoiThan1 = new ThongTinNguoiThan()
            {
                IdNguoiThanNavigation = NguoiThan1,
                SoCCCDNavigation = sinhVien,
            };
            var ThongTinNguoiThan2 = new ThongTinNguoiThan()
            {
                IdNguoiThanNavigation = NguoiThan2,
                SoCCCDNavigation = sinhVien,
            };
            context.Add(ThongTinNguoiThan1);
            context.Add(ThongTinNguoiThan2);
            return Save();
        }
    }
}
