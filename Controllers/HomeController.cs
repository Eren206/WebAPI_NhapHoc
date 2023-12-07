using Microsoft.AspNetCore.Mvc;

namespace testKetNoi.Controllers
{
    [Route("test")]
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("thành công");
        }
    }
}
