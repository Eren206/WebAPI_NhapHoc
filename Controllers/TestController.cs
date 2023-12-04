using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using testKetNoi.Dto;
using testKetNoi.Interfaces;
using testKetNoi.Models;
namespace testKetNoi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        public ISinhVienRepository SinhVienRepository { get; }
        public IMapper mapper;
        public INguoiThanRepository NguoiThanRepository { get; }
        public TestController(ISinhVienRepository SinhVienRepository, INguoiThanRepository NguoiThanRepository, IMapper mapper)
        {
            this.SinhVienRepository = SinhVienRepository;
            this.NguoiThanRepository = NguoiThanRepository;
            this.mapper = mapper;
        }
        // hàm Create thông tin người thân OK 
        [HttpPost("{cccd}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTTNguoiThan(string cccd
            )
        {
            int id1 = 2;
            int id2 = 3;
            if (!NguoiThanRepository.createThongTinNguoiThan(id1,id2,cccd))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
        [HttpGet]
        public IActionResult getidNT (string cccd)
        {
            var a = NguoiThanRepository.IsCreated(cccd);
            return Ok(a);
        }
    }
}
