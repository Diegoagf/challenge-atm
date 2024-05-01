using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Response
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }

        public int ExpireInMinutes { get; set; }

        public AuthenticationResponse(string token, int expireInMinutes)
        {
            Token = token;
            ExpireInMinutes = expireInMinutes;
        }

       

    }
}
