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
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
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

        //public void Seed()
        //{
        // var users = new List<User>
        //    {
        //        new User
        //        {
        //            Name = "Juan Perez",
        //            Rol = "Cliente",
        //            Cards= new List<Card>()
        //            {
        //                new()
        //                {
        //                    CardNumber = "4970110000000062",
        //                    Pin = 1234,
        //                    AccountNumber= "01865281110786583688",
        //                    IsBlocked = false,
        //                    Balance = 0,                                     
        //                }
        //            }
        //        },
        //        new User 
        //        { 
        //            Name = "Maria Gonzalez",
        //            Rol = "Administrador" ,
        //            Cards = new List<Card>()
        //            {
        //                new()
        //                {
        //                    CardNumber = "36230000000019",
        //                    Pin = 0001,
        //                    AccountNumber= "31831767132372861697",
        //                    IsBlocked = false,
        //                    Balance = 0,
        //                }
        //            }}
        //    };

        //  Users.AddRange(users);
        //  SaveChangesAsync();
        //}
    }
}
