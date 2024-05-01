using Challenge.Atm.Domain.Entities;
using Challenge.Atm.Domain.Enums;
using System;

public class Transaction: IAuditableEntity
{
    public int Id { get; set; }
    public decimal Amount { get; set; }

    public TransactionType Type { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime LastModified { get; set; }
}
