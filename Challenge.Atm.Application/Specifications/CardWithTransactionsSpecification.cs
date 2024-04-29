using Ardalis.Specification;
using System;

public class CardWithTransactionsSpecification : Specification<Card>
{
    public CardWithTransactionsSpecification(int pageSize, int pageNumber, string cardNumber)
    {
        Query.Skip((pageNumber - 1) * pageSize)
        .Take(pageNumber);

        if (cardNumber != null)
        {
            Query.Where(c => c.CardNumber == cardNumber)
                .Include(c => c.Transactions);
        }

    }
}
