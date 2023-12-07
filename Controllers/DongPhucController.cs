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
    public class DongPhucController : Controller
    {
        public ISinhVienRepository SinhVienRepository { get; }
        public IDongPhucRepository DongPhucRepository { get; }
        public IMapper mapper;
        public DongPhucController(ISinhVienRepository sinhVienRepository, IDongPhucRepository dongPhucRepository,IMapper mapper)
        {
            this.SinhVienRepository = sinhVienRepository;
            this.DongPhucRepository = dongPhucRepository;
            this.mapper=mapper;
        }
        [HttpGet("/dongphucs")]
        [ProducesResponseType(200,Type=typeof(IEnumerable<DongPhucDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetDanhSachDongPhuc()
        {
            var dsDongPhuc = mapper.Map<List<DongPhucDto>>(DongPhucRepository.getAll());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(dsDongPhuc);
        }
        [HttpPost("/muadongphuc/{cccd}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult MuaDongPhuc(string cccd, [FromBody] MuaDongPhucDto muaDongPhuc)
        {
            var mappedMuaDongPhuc = mapper.Map<MuaDongPhuc>(muaDongPhuc);
            if (muaDongPhuc == null)
            {
                return BadRequest(ModelState);
            }
            if (!SinhVienRepository.SinhVienExists(cccd))
            {
                return NotFound();
            }
            if (cccd != muaDongPhuc.SoCCCD) return BadRequest();
            if (DongPhucRepository.daMuaDongPhuc(mappedMuaDongPhuc))
            {
                if (DongPhucRepository.chinhSuaDongPhuc(mappedMuaDongPhuc))
                {
                    return Ok();
                }
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật hồ sơ, ko ht update");
                return StatusCode(500, ModelState);
            }
            if (!DongPhucRepository.muaDongPhuc(mappedMuaDongPhuc))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật hồ sơ, ko ht create");
                return StatusCode(500, ModelState);
            }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok();
        }
    }
}
