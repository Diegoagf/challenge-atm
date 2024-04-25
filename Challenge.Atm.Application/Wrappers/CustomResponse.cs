using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Wrappers
{
    public class CustomResponse
    {
        public bool Succeded { get; set; }
        public string Message { get; set; }
        public List<string>? Errors { get; set; }

    }
}
