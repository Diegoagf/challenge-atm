using AutoMapper;
using Challenge.Atm.Application.Handlers;
using Challenge.Atm.Application.Requests;
using Challenge.Atm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Mappers
{
    public class GeneralProfile: Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateClientRequest, User>();
        }
    }
}
