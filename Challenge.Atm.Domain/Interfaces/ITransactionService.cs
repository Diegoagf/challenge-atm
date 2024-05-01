using Challenge.Atm.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Domain.Interfaces
{
    public interface ITransactionService
    {
        Task<Transaction> CreateAsync(string cardNumber, string userName, decimal amount, string type, CancellationToken ct);

        Task<List<Transaction>> GetTransactionsAsync(int pagedSize, int pageNumber, string cardNumber, CancellationToken ct);
    }
}
