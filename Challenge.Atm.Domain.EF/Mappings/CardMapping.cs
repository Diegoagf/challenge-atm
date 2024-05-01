using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Atm.Domain.Entities;

namespace Challenge.Atm.Domain.EF.Mappings
{
    public class CardMapping : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CardNumber).IsRequired().HasMaxLength(16);
            builder.Property(c => c.AccountNumber).IsRequired().HasMaxLength(20);
            builder.Property(c => c.Pin).IsRequired();
            builder.Property(c => c.IsBlocked).IsRequired();
            builder.Property(c => c.Balance).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasIndex(c => c.CardNumber);

        }
    }
}
