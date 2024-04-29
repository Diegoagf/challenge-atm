using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Domain.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Card card);

        (string cardNumber, string OwnerName) ValidateToken(string token);
    }
}
