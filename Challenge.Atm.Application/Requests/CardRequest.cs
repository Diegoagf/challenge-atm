using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Requests
{
    public class CardRequest
    {
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }

        public int Pin { get; set; }
    }
}
