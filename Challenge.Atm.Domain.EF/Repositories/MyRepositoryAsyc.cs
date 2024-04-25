using Ardalis.Specification.EntityFrameworkCore;
using Challenge.Atm.Domain.EF.DBContexts;
using Challenge.Atm.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Domain.EF.Repositories
{
    public class MyRepositoryAsyc<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbcontext;
        public MyRepositoryAsyc(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbcontext = dbContext;
        }
    }
}
