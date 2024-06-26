﻿using Challenge.Atm.Application.Handlers.Commnads;
using Challenge.Atm.Application.Handlers.Queries;
using Challenge.Atm.Application.Mappers;
using Challenge.Atm.Application.Services;
using Challenge.Atm.Application.Validations;
using Challenge.Atm.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Installers
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateCardCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateTransactionValidator>();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<LoginCommand>();
                cfg.RegisterServicesFromAssemblyContaining<CreateCardCommand>();
                cfg.RegisterServicesFromAssemblyContaining<CreateTransactionCommand>();
                cfg.RegisterServicesFromAssemblyContaining<GetHistoryTransactionsQuery>();
                cfg.RegisterServicesFromAssemblyContaining<GetBalanceQuery>();
                cfg.RegisterServicesFromAssemblyContaining<GetAllCardsQuery>();
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });
            services.AddAutoMapper(typeof(CardMapper));
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ITransactionService, TransactionService>();

        }
    }
}
