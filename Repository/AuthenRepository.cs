using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using testKetNoi.Data;
using testKetNoi.Interfaces;
using testKetNoi.Models;

namespace testKetNoi.Repository
{
    public class AuthenRepository : IAuthenRepository
    {
        private IConfiguration configuration;
        private readonly DataContext context;

        public AuthenRepository(IConfiguration configuration, DataContext context)
        {
            this.configuration = configuration;
            this.context = context;

        }
        public string generateToken(TaiKhoan taiKhoan)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    
                    new Claim("UserName", taiKhoan.SoCCCD),

                    //roles-chưa dùng đến

                    //new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                //thời gian hết hạn token
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }

        public TaiKhoan getValidUser(string soCccd, string matKhau)
        {
            var sv = context.SinhVien.SingleOrDefault(s => s.SoCCCD == soCccd && s.MatKhau==matKhau);
            if (sv == null) return null;
            TaiKhoan tk = new TaiKhoan();
            tk.SoCCCD = sv.SoCCCD;
            tk.MatKhau = sv.MatKhau;
            return tk;
        }
    }
}
