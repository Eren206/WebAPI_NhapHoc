using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using testKetNoi.Interfaces;
using Microsoft.EntityFrameworkCore;
using testKetNoi.Models.Response;
using testKetNoi.Models;

namespace testKetNoi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : Controller
    {
        public IAuthenRepository AuthenRepository { get; }
        public LoginController(IAuthenRepository authenRepository)
        {
            this.AuthenRepository = authenRepository;
        }
        [HttpPost]
        public IActionResult Validate([FromBody] TaiKhoan taiKhoan)
        {
            var tk = AuthenRepository.getValidUser(taiKhoan.SoCCCD, taiKhoan.MatKhau);
            if (tk == null) //không đúng tài khoản và mật khẩu
            {
                return Ok(new LoginAPI
                {
                    success = false,
                    message = "Invalid username/password"
                });
            }

            //cấp token

            return Ok(new LoginAPI
            {
                success = true, 
                message = "Authenticate success",
                data = AuthenRepository.generateToken(tk)
            });
        }
    }
}
