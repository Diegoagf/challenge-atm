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
        private readonly IJwtService _jwtService;

        public LoginService(IConfiguration config, IJwtService jwtService)
        {
            _config = config;
            _jwtService = jwtService;
        }

        public Task<string> Login(string cardNumber, int pin, CancellationToken ct)
        {
          
           
            return Task.FromResult (_jwtService.GenerateToken(cardNumber));
        }
    }
}
