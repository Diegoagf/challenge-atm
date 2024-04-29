using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Response
{
    public class CardResponse
    {
        public string CardNumber { get; set; }

        public string AccountNumber { get; set; }

        public List<TransactionResponse> Transactions { get; set; }

        public decimal Balance { get; set; }
    }
}
