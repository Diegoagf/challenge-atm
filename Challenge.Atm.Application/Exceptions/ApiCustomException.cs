using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Exceptions
{
    public class ApiCustomException: Exception
    {
        public HttpStatusCode statusCode;
        public ApiCustomException(string message, HttpStatusCode httpStatusCode ) : base(message)
        {
            statusCode = httpStatusCode;
        }
 
    }
}
