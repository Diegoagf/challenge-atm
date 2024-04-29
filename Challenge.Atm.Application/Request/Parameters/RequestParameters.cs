using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Request.Parameters
{
    public class RequestParameters
    {
        public int PageNumber { get; set; }
        public int PagedSize { get; set; }
        public RequestParameters()
        {
            PageNumber = 1;
            PagedSize = 10;  
        }
        public RequestParameters(int pageNumber, int pagedSize)
        {
           PageNumber = pageNumber <1 ? 1 : pageNumber;
           PagedSize =pagedSize >10 ? 10 : pagedSize;    
        }
    }
}
