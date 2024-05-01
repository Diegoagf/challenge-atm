using Challenge.Atm.Domain.Entities;
using Microsoft.AspNetCore.Identity;

public class Card: AuditableEntity
{
    public string OwnerName { get; set; }
    public string CardNumber { get; set; }
    public int Pin { get; set; }
    public string AccountNumber { get; set; }
    public bool IsBlocked { get; set; }
    public decimal Balance { get; set; }
    public List<Transaction>? Transactions { get; set; }
}
