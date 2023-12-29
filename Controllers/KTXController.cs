using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testKetNoi.Dto;
using testKetNoi.Interfaces;
using testKetNoi.Models;
using testKetNoi.Repository;

namespace testKetNoi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KTXController : Controller
    {
        public IKTXRepository KTXRepository { get; }
        public IMapper mapper { get; }
        public ISinhVienRepository SinhVienRepository { get; }
        public KTXController(IKTXRepository KTXRepository,IMapper mapper, ISinhVienRepository SinhVienRepository)
        {
            this.KTXRepository = KTXRepository;
            this.mapper = mapper;
            this.SinhVienRepository = SinhVienRepository;
        }
        [HttpGet("danhsachktx")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<KTX>))]
        public IActionResult GetKTXs()
        {
            var ktxs =mapper.Map<List<KTXDto>>(KTXRepository.getAll());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(ktxs);
        }
        [HttpPost("dangky/{cccd}")]
        public IActionResult DangKyKTX(string cccd, [FromBody] DangKyKTXDto dangKyDto)
        {
            var dangKy = mapper.Map<DangKyKTX>(dangKyDto);
            if (dangKyDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!SinhVienRepository.SinhVienExists(cccd))
            {
                return NotFound();
            }
            if (cccd != dangKy.SoCCCD) return BadRequest();
            if (KTXRepository.isDuplicate(cccd, dangKy.MaPhong))
            {
                return Ok("Sinh viên đã đăng ký phòng này!");
            }
            if (KTXRepository.isRes(cccd))
            {
                if (KTXRepository.updateKTX(dangKy))
                {
                    return Ok();
                }
                ModelState.AddModelError("", "Có lỗi xảy ra khi chỉnh sửa thông tin đăng ký");
                return StatusCode(500, ModelState);
            }
            if (!KTXRepository.dangKy(dangKy))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi đăng ký");
                return StatusCode(500, ModelState);
            }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok();
        }
    }
}
