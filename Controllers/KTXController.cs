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

        public KTXController(IKTXRepository KTXRepository) {
            this.KTXRepository= KTXRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<KTX>))]
        public IActionResult GetKTXs()
        {
            var sinhViens =KTXRepository.GetKTXs();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(sinhViens);
        }
        [HttpGet("{maKTX}")]
        [ProducesResponseType(200, Type = typeof(KTX))]
        public IActionResult GetKTX(string maKTX)
        {
            if (!KTXRepository.KTXExists(maKTX))
            {
                return NotFound();
            }
            var KTX = KTXRepository.GetKTX(maKTX);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(KTX);
        }
    }
}
