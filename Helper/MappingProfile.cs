using AutoMapper;
using testKetNoi.Dto;
using testKetNoi.Models;

namespace testKetNoi.Helper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<SinhVien, SinhVienDto>();CreateMap<SinhVienDto, SinhVien>();
            CreateMap<SinhVien,SinhVienNTDto>(); CreateMap<SinhVienNTDto, SinhVien >();
            CreateMap<SinhVienDto, SinhVienNTDto>(); CreateMap<SinhVienNTDto ,SinhVienDto>();
            CreateMap<KTX, KTXDto>();   
            CreateMap<NguoiThan,NguoiThanDto>(); CreateMap<NguoiThanDto, NguoiThan>();
            CreateMap<HoaDon,HoaDonDto>(); CreateMap<HoaDonDto, HoaDon>();
            CreateMap<HoSoSinhVien, HoSoSinhVienDto>(); CreateMap<HoSoSinhVienDto,HoSoSinhVien >();
            CreateMap<DongPhuc,DongPhucDto>();CreateMap<DongPhucDto, DongPhuc>();  
            CreateMap<MuaDongPhuc ,MuaDongPhucDto>(); CreateMap<MuaDongPhucDto, MuaDongPhuc>();
            CreateMap<DangKyKTX, DangKyKTXDto>();CreateMap<DangKyKTXDto ,DangKyKTX>();
        }
    }
}
