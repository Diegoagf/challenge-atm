using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Domain.Interfaces
{
    public interface ILoginService
    {
       Task<string> Login(Card card, CancellationToken ct);

        (string cardNumber, string OwnerName) ValidateCard();
    }
}
