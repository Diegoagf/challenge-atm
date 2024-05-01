using Ardalis.Specification;
using Challenge.Atm.Domain.Entities;
using System.Xml.Linq;

public class CardSpecification : Specification<Card>
{
    public CardSpecification(string cardNumber)
    {
        if (cardNumber != null)
        {
            Query.Where(c => c.CardNumber == cardNumber);
        }

    }
}
