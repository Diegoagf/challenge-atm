using AutoMapper;
using Challenge.Atm.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Mappers
{
    public class TransactionMapper: Profile
    {
        public TransactionMapper()
        {
            CreateMap<Transaction, TransactionResponse>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        }
    }
}
