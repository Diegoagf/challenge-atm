using Challenge.Atm.Application.Exceptions;
using Challenge.Atm.Domain.Enums;
using Challenge.Atm.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepositoryAsync<Card> _cardRepository;
        public TransactionService(IRepositoryAsync<Card> cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<Transaction> CreateAsync(string cardNumber, string userName,decimal amount, string type, CancellationToken ct)
        {
            var card = await _cardRepository.FirstOrDefaultAsync(new CardWithTransactionsSpecification(cardNumber));

            TransactionType typeEnum = Enum.TryParse(type, out TransactionType result) ? result : default;

            if(typeEnum == TransactionType.Extraction && amount > card!.Balance)
            {
                throw new ApiCustomException("Insufficient funds", HttpStatusCode.Forbidden);
            }

            var newTransaction = new Transaction
            {
                Amount = amount,
                Type = typeEnum,
                CreatedBy = userName,
                LastModifiedBy = userName,
                LastModified = DateTime.UtcNow,
            };
            if (card!.Transactions == null)
            {
                card.Transactions = new List<Transaction>();
            }
            card!.Transactions.Add(newTransaction);

            var amountWithSign = typeEnum == TransactionType.Deposit ? amount : -amount;
            card!.Balance += amountWithSign;
            await _cardRepository.UpdateAsync(card, ct);

            return newTransaction;
        }

        public async Task<List<Transaction>?> GetTransactionsAsync(int pageSize, int pageNumber, string cardNumber,CancellationToken ct)
        {
            var cardWithTransactions = await _cardRepository.FirstOrDefaultAsync(new CardWithTransactionsSpecification(cardNumber), ct);
            if(cardWithTransactions?.Transactions != null)
            {
                return cardWithTransactions!.Transactions
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            return cardWithTransactions?.Transactions;
        }
    }
}
