using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{cccd}/TongHocPhi")]
        [ProducesResponseType(200, Type = typeof(decimal))]
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
        [HttpGet("{cccd}/ChiTietHP")]
        public IActionResult GetChiTietHocPhi(string cccd)
        {
            if (!SinhVienRepository.SinhVienExists(cccd))
            {
                return NotFound();
            }
            return Ok(HocPhiRepository.getChiTietHocPhi(cccd));
        }
    }
}
