using Challenge.Atm.Domain.Entities;
using Microsoft.AspNetCore.Identity;

public class Card: IAuditableEntity
{
    public int Id { get; set; }

    public string OwnerName { get; set; }
    public string CardNumber { get; set; }
    public int Pin { get; set; }

    public string AccountNumber { get; set; }

    public bool IsBlocked { get; set; }

    public decimal Balance { get; set; }

    public List<Transaction>? Transactions { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime LastModified { get; set; }
}
