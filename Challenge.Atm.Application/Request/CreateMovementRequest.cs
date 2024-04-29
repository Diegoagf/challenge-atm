using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Request
{
    public class CreateMovementRequest
    {
        public string CardNumber { get; set; }

        public decimal Amount { get; set; }

        public string Type { get; set; }
    }
}
