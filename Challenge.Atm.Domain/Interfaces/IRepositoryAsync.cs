using Ardalis.Specification;
using Challenge.Atm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Domain.Interfaces
{
    public interface IRepositoryAsync<T>: IRepositoryBase<T> where T : class
    {
    }

    public interface IReadRepositoryAsync<T> : IReadRepositoryBase<T> where T : class
    {
    
    }
}
