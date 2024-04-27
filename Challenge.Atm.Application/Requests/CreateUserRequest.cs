using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Requests
{
    public class CreateUserRequest
    {
        public string Name { get; set; }

        public string Rol { get; set; }

        public List<CardRequest>? Card { get; set; }
    }
}
