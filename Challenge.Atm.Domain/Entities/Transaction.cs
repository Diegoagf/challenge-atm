using Challenge.Atm.Domain.Entities;
using Challenge.Atm.Domain.Enums;
using System;

public class Transaction: AuditableEntity
{
    public decimal Amount { get; set; }

    public TransactionType Type { get; set; }
}
