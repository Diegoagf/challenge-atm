using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Response
{
    public class CardResponse
    {
        public string Name { get; set; }
        public string CardNumber { get; set; }

        public string AccountNumber { get; set; }

        public DateTime? LastTransactionAt { get; set; }

        public decimal Balance { get; set; }
    }
}
