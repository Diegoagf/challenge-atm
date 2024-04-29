using Challenge.Atm.Application.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Requests
{
    public class CardRequest: CardBaseRequest
    {
        public string OwnerName { get; set; }
        public string AccountNumber { get; set; }
    }
}
