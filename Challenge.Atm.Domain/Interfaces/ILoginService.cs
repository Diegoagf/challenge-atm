using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Domain.Interfaces
{
    public interface ILoginService
    {
        Task<string> Login(string cardNumber, int pin, CancellationToken ct);
    }
}
