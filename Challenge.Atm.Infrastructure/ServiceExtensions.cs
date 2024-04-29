using Challenge.Atm.Application.Wrappers;
using Challenge.Atm.Domain.EF.DBContexts;
using Challenge.Atm.Domain.EF.Repositories;
using Challenge.Atm.Domain.Entities;
using Challenge.Atm.Domain.Interfaces;
using Challenge.Atm.Domain.Settings;
using Challenge.Atm.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace Challenge.Atm.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddDbContextInfraestructure( this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null)
                        .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                )
            );
            
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsyc<>));
            services.AddTransient(typeof(IReadRepositoryAsync<>), typeof(MyReadRepositoryAsyc<>));
            services.AddTransient<IJwtService, JwtService>();
            services.AddMemoryCache();
        }

        public static void AddAuthenticationInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddIdentity<Card, Microsoft.AspNet.Identity.EntityFramework.IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            //services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o=>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["Jwt:Isuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "application/json";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonSerializer.Serialize(new CustomResponse<string>(false,"Unauthorized"));
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {          
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonSerializer.Serialize(new CustomResponse<string>(false, "Unauthorized"));
                        return context.Response.WriteAsync(result);
                    }
                };
            });

        }
    }
}
