using Ardalis.Specification;
using Challenge.Atm.Domain.Entities;

public class UsersWithCardsSpecification : Specification<User>
{
    public UsersWithCardsSpecification()
    {
        Query.Include(user => user.Cards);
    }
}
