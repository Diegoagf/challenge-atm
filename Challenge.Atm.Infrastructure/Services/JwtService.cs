using Challenge.Atm.Application.Exceptions;
using Challenge.Atm.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Challenge.Atm.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private const string NameClaimCardNumber = "card_number";
        private const int DefautDurationInMinutes = 15;
        private readonly IConfiguration _config;
        public JwtService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateToken(Card card)
        {

            var claims = new[]
              {
                 new Claim(NameClaimCardNumber, card.CardNumber),
                 new Claim(JwtRegisteredClaimNames.Name, card.OwnerName),
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expires = Int32.TryParse(_config["Jwt:DurationInMinutes"], out int parsedExpires) ? parsedExpires : DefautDurationInMinutes;

            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expires),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public (string cardNumber, string OwnerName) ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                    ValidateIssuer = false, 
                    ValidateAudience = false
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                var cardNumberClaim = principal.FindFirst(NameClaimCardNumber)?.Value;
                var nameClaim = principal.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
                return (cardNumberClaim, nameClaim);
                
            }
            catch (Exception ex)
            {
                throw new ApiCustomException(ex.Message, HttpStatusCode.Forbidden);
            }
        }
    }
}
