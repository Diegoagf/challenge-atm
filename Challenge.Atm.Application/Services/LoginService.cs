using Challenge.Atm.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Challenge.Atm.Application.Services
{
    public class LoginService: ILoginService
    {
        private readonly IConfiguration _config;
       
        public LoginService(IConfiguration config)
        {
            _config = config;
        }

        public Task<string> Login(string cardNumber, int pin, CancellationToken ct)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("cardNumber", cardNumber),
           new Claim(ClaimTypes.Name, "Diego")
            };

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
