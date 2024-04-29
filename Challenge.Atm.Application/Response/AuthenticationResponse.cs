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
        public string CardNumber { get; set; }

        public string Email { get; set; }

        public string JwToken { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

    }
}
