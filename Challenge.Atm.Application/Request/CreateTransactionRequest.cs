using Challenge.Atm.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Request
{
    public class CreateTransactionRequest
    {
        public string? CardNumber { get; set; }

        public decimal Amount { get; set; }

        public string Type { get; set; }
    }
}
