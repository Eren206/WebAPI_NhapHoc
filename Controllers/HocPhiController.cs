using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using testKetNoi.Data;
using testKetNoi.Dto;
using testKetNoi.Interfaces;
using testKetNoi.Models;
using testKetNoi.Repository;

namespace testKetNoi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocPhiController :Controller
    {
        public ISinhVienRepository SinhVienRepository { get; }
        public IHocPhiRepository HocPhiRepository { get; }
        public IMapper mapper;
        public HocPhiController(ISinhVienRepository sinhVienRepository, IHocPhiRepository hocPhiRepository, IMapper mapper)
        {
            this.SinhVienRepository = sinhVienRepository;
            this.HocPhiRepository = hocPhiRepository;
            this.mapper = mapper;
        }
        [HttpGet("tonghocphi/{cccd}")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetTongHocPhi(string cccd)
        {
            if (!SinhVienRepository.SinhVienExists(cccd))
            {
                return NotFound();
            }
            decimal hocPhi = HocPhiRepository.getTongHocPhi(cccd);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(hocPhi);
        }
        [HttpGet("chitiethp/{cccd}")]
        [ProducesResponseType(200, Type = typeof(ChiTietHocPhiDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetChiTietHocPhi(string cccd)
        {
            if (!SinhVienRepository.SinhVienExists(cccd))
            {
                return NotFound();
            }
            var cthp = HocPhiRepository.getChiTietHocPhi(cccd);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(cthp);
        }
        [HttpGet("hoadon/{cccd}")]
        [ProducesResponseType(200,Type=typeof(HoaDonDto))]
        [ProducesResponseType(404)]
        public IActionResult GetHoaDon(string cccd)
        {
            if (!SinhVienRepository.SinhVienExists(cccd))
            {
                return NotFound();
            }
            if (!SinhVienRepository.isPay(cccd))
            {
                return NotFound("Không tìm thấy hóa đơn/Chưa thanh toán");
            }
            var sv = SinhVienRepository.GetSinhVienByCCCD(cccd);
            var hd = HocPhiRepository.GetHoaDon(sv.MaHD);
            var hdMapped = mapper.Map<HoaDonDto>(hd);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(hdMapped);
        }
    }
}
