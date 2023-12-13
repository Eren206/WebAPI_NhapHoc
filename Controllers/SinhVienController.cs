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
    public class SinhVienController : Controller
    {
        public ISinhVienRepository SinhVienRepository { get; }  
        public IMapper mapper;
        public INguoiThanRepository NguoiThanRepository { get; }
        public SinhVienController(ISinhVienRepository SinhVienRepository,INguoiThanRepository NguoiThanRepository,IMapper mapper)
        {
            this.SinhVienRepository = SinhVienRepository;
            this.NguoiThanRepository = NguoiThanRepository;
            this.mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SinhVien>))]
        public IActionResult GetSinhViens()
        {
            var sinhViens = mapper.Map<List<SinhVienDto>>(SinhVienRepository.GetSinhViens());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(sinhViens);
        }

        [HttpGet("{cccd}")]
        [ProducesResponseType(200, Type = typeof(SinhVienNTDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetPhieuSinhVien(string cccd)
        {

            if (!SinhVienRepository.SinhVienExists(cccd))
            {
                return NotFound();
            }

            var SinhVien = mapper.Map<SinhVienNTDto>(SinhVienRepository.GetSinhVienByCCCD(cccd));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (NguoiThanRepository.IsCreated(cccd))
            {
                var nt = NguoiThanRepository.getIDNguoiThans(cccd);
                SinhVien.NguoiThan1 = mapper.Map<NguoiThanDto>(NguoiThanRepository.GetNguoiThanById(nt.Item1));
                SinhVien.NguoiThan2 = mapper.Map<NguoiThanDto>(NguoiThanRepository.GetNguoiThanById(nt.Item2));

            }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(SinhVien);
        }
        [HttpPut("updateSV/{cccd}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public  IActionResult UpdateSinhVien(string cccd,
            [FromBody] SinhVienNTDto updatedSinhVien)
        {   
            if(updatedSinhVien== null)
            {
                return BadRequest(ModelState);
            }
            if(cccd != updatedSinhVien.SoCCCD)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(); 
            }
            var sinhVienMap = mapper.Map<SinhVien>(updatedSinhVien);
            if(NguoiThanRepository.IsCreated(cccd))
            {
                var nt1 = mapper.Map<NguoiThan>(updatedSinhVien.NguoiThan1);
                var nt2 = mapper.Map<NguoiThan>(updatedSinhVien.NguoiThan2);
                var tupleId = NguoiThanRepository.getIDNguoiThans(cccd);
                nt1.IdNguoiThan = tupleId.Item1;
                nt2.IdNguoiThan = tupleId.Item2;
                if (!(NguoiThanRepository.UpdateNguoiThan(nt1)&& 
                    NguoiThanRepository.UpdateNguoiThan(nt2)))
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật sinh viên");
                    return StatusCode(500, ModelState);
                }
            }
            else {
                int nguoiThan1ID = NguoiThanRepository.CreateNguoiThan(mapper.Map<NguoiThan>(updatedSinhVien.NguoiThan1));
                int nguoiThan2ID = NguoiThanRepository.CreateNguoiThan(mapper.Map<NguoiThan>(updatedSinhVien.NguoiThan2));
                if (!NguoiThanRepository.createThongTinNguoiThan(nguoiThan1ID, nguoiThan2ID, cccd))
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật sinh viên");
                    return StatusCode(500, ModelState);
                }
            }
            if (!SinhVienRepository.UpdateSinhVien(sinhVienMap))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật sinh viên");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
        [HttpPost("hoso/{cccd}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult CreateHoSo(string cccd,[FromBody] HoSoSinhVienDto hoSoSV)
        {
            var mappedHoSoSV = mapper.Map<HoSoSinhVien>(hoSoSV);
            if (hoSoSV == null)
            {
                return BadRequest(ModelState);
            }
            if (!SinhVienRepository.SinhVienExists(cccd))
            {
                return NotFound();
            }
            if(cccd != hoSoSV.SoCCCD) return BadRequest();
            if (SinhVienRepository.isHoSoRegister(mappedHoSoSV))
            {
                if (SinhVienRepository.updateHoSoSinhVien(mappedHoSoSV))
                {
                    return Ok();
                }
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật hồ sơ");
                return StatusCode(500, ModelState);
            }
            if (!SinhVienRepository.createHoSoSinhVien(mappedHoSoSV))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật hồ sơ");
                return StatusCode(500, ModelState);
            }
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok();
        }
    }
}
