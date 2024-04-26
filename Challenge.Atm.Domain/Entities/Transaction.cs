using Challenge.Atm.Domain.Entities;
using System;

public class Transaction: AuditableEntity
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
}
