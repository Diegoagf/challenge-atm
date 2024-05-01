using Challenge.Atm.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Wrappers
{
    public class PagedResponse<T> : CustomResponse<T>
    {
        public int PageNumber { get; set; }
        public int PagedSize { get; set; }

        public PagedResponse(T data,int pageNumber, int pagedSize = 10) 
        {
            PageNumber = pageNumber;
            PagedSize = pagedSize;
            Data = data;
            Errors = null;
        }
    }
}
