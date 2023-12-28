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
        public ISinhVienRepository SinhVienRepository { get; }
        public LoginController(IAuthenRepository authenRepository, ISinhVienRepository sinhVienRepository)
        {
            this.AuthenRepository = authenRepository;
            this.SinhVienRepository = sinhVienRepository;
        }
        [HttpPost]
        public IActionResult Validate([FromBody] TaiKhoan taiKhoan)
        {
            if (!SinhVienRepository.SinhVienExists(taiKhoan.SoCCCD))
            {
                return Ok(new LoginAPI
                {
                    success = false,
                    message = "Invalid username/password"
                });
            }
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
