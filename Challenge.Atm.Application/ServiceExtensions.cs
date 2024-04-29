using Challenge.Atm.Application.Handlers;
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

namespace Challenge.Atm.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration) 
        {

            services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateCardCommandValdator>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<LoginCommand>();
                cfg.RegisterServicesFromAssemblyContaining<CreateCardCommand>();
                cfg.RegisterServicesFromAssemblyContaining<CreateMovementCommand>();
                cfg.RegisterServicesFromAssemblyContaining<GetHistoryCardQuery>();
                cfg.RegisterServicesFromAssemblyContaining<GetBalanceQuery>();
                cfg.RegisterServicesFromAssemblyContaining<GetAllCardsQuery>();
                //cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });
            services.AddAutoMapper(typeof(CardMapper));
            services.AddScoped<ILoginService, LoginService>();

        }
    }
}
