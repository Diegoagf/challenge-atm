using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Requests
{
    public class LoginRequest
    {
        public required string CardNumber { get; set; }

        public required int Pin { get; set; }

    }
}
