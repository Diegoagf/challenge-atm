using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Response
{
    public class UserResponse
    {
        public string Name { get; set; }

        public string Rol { get; set; }

        public List<CardResponse>? Cards { get; set; }
    }
}
