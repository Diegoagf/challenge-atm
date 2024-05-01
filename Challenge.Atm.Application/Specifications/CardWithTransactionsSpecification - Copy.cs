using Ardalis.Specification;
using System.Linq;

public class CardWithTransactionsSpecification : Specification<Card>
{
    public CardWithTransactionsSpecification(string cardNumber)
    {
        if (cardNumber != null)
        {
            Query.Where(c => c.CardNumber == cardNumber);
            Query.Include(c => c.Transactions);
        }
    }
}
