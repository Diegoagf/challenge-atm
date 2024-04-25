using Challenge.Atm.Domain.EF.DBContexts;
using Challenge.Atm.Domain.EF.Repositories;
using Challenge.Atm.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddDbContextInfraestructure( this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsyc<>));
        }
    }
}
