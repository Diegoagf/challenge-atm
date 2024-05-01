using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Response
{
    public class CardWithPinResponse: CardResponse
    {
        public int Pin { get; set; }
    }
}
