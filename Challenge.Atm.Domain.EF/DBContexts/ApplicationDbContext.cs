using Challenge.Atm.Domain.EF.Mappings;
using Challenge.Atm.Domain.Entities;
using Challenge.Atm.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Challenge.Atm.Domain.EF.DBContexts
{
    public class ApplicationDbContext: DbContext
    {
        private readonly DateTime _dateTime = DateTime.UtcNow;
       
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = _dateTime;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CardMapping());
            modelBuilder.ApplyConfiguration(new TransactionMapping());
        }
    }
}
