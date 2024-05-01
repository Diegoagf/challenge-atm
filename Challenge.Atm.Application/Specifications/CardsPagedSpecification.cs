using Ardalis.Specification;
using Challenge.Atm.Domain.Entities;
using System.Xml.Linq;

public class CardsPagedSpecification : Specification<Card>
{
    public CardsPagedSpecification(int pageSize, int pageNumber,string name)
    {
        Query.Skip((pageNumber - 1)* pageSize)
            .Take(pageSize);
        
        if (name != null)
        { 
            Query.Where(c => c.OwnerName == name);
        }

    }
}
