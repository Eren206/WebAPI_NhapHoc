using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using testKetNoi.Dto;
using testKetNoi.Interfaces;
using testKetNoi.Models;

namespace testKetNoi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiThanController : Controller
    {
        public ISinhVienRepository SinhVienRepository { get; }
        public IMapper mapper;
        public INguoiThanRepository NguoiThanRepository { get; }
        public NguoiThanController(ISinhVienRepository SinhVienRepository, INguoiThanRepository NguoiThanRepository, IMapper mapper)
        {
            this.SinhVienRepository = SinhVienRepository;
          
            this.NguoiThanRepository = NguoiThanRepository;
            this.mapper = mapper;
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateNguoiThan(
            [FromBody] NguoiThanDto createNguoiThan)
        {
            if (createNguoiThan == null)
                return BadRequest(ModelState);
            var NguoiThanMap = mapper.Map<NguoiThan>(createNguoiThan);


            if (NguoiThanRepository.CreateNguoiThan(NguoiThanMap) == -1)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }
    }
}
