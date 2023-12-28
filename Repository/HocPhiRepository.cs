using testKetNoi.Data;
using testKetNoi.Dto;
using testKetNoi.Interfaces;
using testKetNoi.Models;

namespace testKetNoi.Repository
{
    public class HocPhiRepository :IHocPhiRepository
    {
        private readonly DataContext context;
        public HocPhiRepository(DataContext context)
        {
            this.context = context;
        }
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public decimal getTongHocPhi(string cccd)
        {
            decimal phiCLC = 0;
            decimal phiKTX = 0;
            decimal phiDongPhuc = 0;
            SinhVien sv = context.SinhVien.Where(s => s.SoCCCD == cccd).FirstOrDefault();
            HocPhiNganh hpn = context.HocPhiNganh.Where(n => n.MaNganh == sv.MaNganh).FirstOrDefault();
            decimal hocPhi = ((decimal)hpn.SoTinChiHK1 * (decimal)hpn.DonGiaTC);
            decimal kinhPhi = (decimal)hpn.KinhPhiNhapHoc;
            decimal bhyt = (decimal)hpn.PhiBHYT;
            decimal bhtn = (decimal)hpn.PhiBHTN;
            var dangKy = context.DangKyKTX.Where(k => k.SoCCCD == sv.SoCCCD).FirstOrDefault();
            if (dangKy != null)
            {
                string maKTX = dangKy.MaPhong;
                KTX ktx = context.KTX.Where(k => k.MaPhong == maKTX).FirstOrDefault();
                phiKTX = ktx.DonGia * 6;
            }

            List<MuaDongPhuc> dongPhucs = context.MuaDongPhuc.Where(s => s.SoCCCD == sv.SoCCCD).ToList();
            if (hpn.MaNganh.Equals("CNTT_CLC"))
            {
                phiCLC = (decimal)hpn.PhiCLC;
            }
            if (dongPhucs.Count() > 0)
            {
                foreach (MuaDongPhuc dongPhuc in dongPhucs)
                {
                    phiDongPhuc += context.DongPhuc.Where(d => d.MaDongPhuc == dongPhuc.MaDongPhuc).FirstOrDefault().DonGia * dongPhuc.SoLuong;
                }
            }
            decimal tongHocPhi = hocPhi + kinhPhi + bhyt + bhtn + phiCLC + phiKTX + phiDongPhuc;
            return tongHocPhi;
        }
        public ChiTietHocPhiDto getChiTietHocPhi(string cccd)
        {

            decimal phiCLC = 0;
            decimal phiKTX = 0;
            decimal phiDongPhuc = 0;
            SinhVien sv = context.SinhVien.Where(s => s.SoCCCD == cccd).FirstOrDefault();
            HocPhiNganh hpn = context.HocPhiNganh.Where(n => n.MaNganh == sv.MaNganh).FirstOrDefault();
            decimal hocPhi = ((decimal)hpn.SoTinChiHK1 * (decimal)hpn.DonGiaTC);
            decimal kinhPhi = (decimal)hpn.KinhPhiNhapHoc;
            decimal bhyt = (decimal)hpn.PhiBHYT;
            decimal bhtn = (decimal)hpn.PhiBHTN;
            var dangKy = context.DangKyKTX.Where(k => k.SoCCCD == sv.SoCCCD).FirstOrDefault();
            
            if (dangKy != null)
            {
                string maKTX = dangKy.MaPhong;
                KTX ktx = context.KTX.Where(k => k.MaPhong == maKTX).FirstOrDefault();
                phiKTX = ktx.DonGia * 6;
            }

            List<MuaDongPhuc> dongPhucs = context.MuaDongPhuc.Where(s => s.SoCCCD == sv.SoCCCD).ToList();
            if (hpn.MaNganh.Equals("CNTT_CLC"))
            {
                phiCLC = (decimal)hpn.PhiCLC;
            }
            if (dongPhucs.Count() > 0)
            {
                foreach (MuaDongPhuc dongPhuc in dongPhucs)
                {
                    phiDongPhuc += context.DongPhuc.Where(d => d.MaDongPhuc == dongPhuc.MaDongPhuc).FirstOrDefault().DonGia * dongPhuc.SoLuong;
                }
            }
            // hocPhi + kinhPhi + bhyt + bhtn + phiCLC + phiKTX+phiDongPhuc;

            return new ChiTietHocPhiDto()
            {
                HocPhi = hocPhi,
                KinhPhiNhapHoc = kinhPhi,
                PhiBHYT = bhyt,
                PhiBHTN = bhtn,
                PhiCLC = phiCLC,
                PhiDongPhuc = phiDongPhuc,
                PhiKTX = phiKTX
            };
        }

        // cần tạo mới bill và cập nhật mã HD cho sinh viên.
        public bool SaveBill(HoaDon hoaDon,string cccd)
        {
            context.HoaDon.Add(hoaDon);
            var sv=context.SinhVien.Where(s => s.SoCCCD == cccd).FirstOrDefault();
            sv.MaHD = hoaDon.MaHD;
            context.SinhVien.Update(sv); 
            return Save();
        }
        public HoaDon GetHoaDon(string maHD)
        {
            return context.HoaDon.Where(h => h.MaHD == maHD).First();
        }
    }
}
