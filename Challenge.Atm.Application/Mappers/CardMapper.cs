using AutoMapper;
using Challenge.Atm.Application.Handlers;
using Challenge.Atm.Application.Requests;
using Challenge.Atm.Application.Response;
using Challenge.Atm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Mappers
{
    public class CardMapper: Profile
    {
        public CardMapper()
        {

            CreateMap<CardRequest, Card>()
             .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(src => false))
             .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => 0))
             .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.OwnerName))
             .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.OwnerName));
            CreateMap<LoginRequest, Card>();


            CreateMap<Card, CardResponse>();

            CreateMap<Transaction, TransactionResponse>();

        }
    }
}
