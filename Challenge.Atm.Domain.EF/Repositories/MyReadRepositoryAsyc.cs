using Ardalis.Specification.EntityFrameworkCore;
using Challenge.Atm.Domain.EF.DBContexts;
using Challenge.Atm.Domain.Interfaces;


namespace Challenge.Atm.Domain.EF.Repositories
{
    public class MyReadRepositoryAsyc<T> : RepositoryBase<T>, IReadRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbcontext;
        public MyReadRepositoryAsyc(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbcontext = dbContext;
        }
    }
}
