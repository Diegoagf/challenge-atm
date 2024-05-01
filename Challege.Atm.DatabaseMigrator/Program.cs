using Challenge.Atm.Domain.EF.DBContexts;
using Challenge.Atm.Domain.Entities;
using Challenge.Atm.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;


namespace DatabaseMigrationAndSeed
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crear un host de la aplicación
            var host = CreateHostBuilder(args).Build();

            Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .CreateLogger();

            // Ejecutar los servicios dentro del alcance del host
            using (var scope = host.Services.CreateScope())
            {
                // Obtener el servicio del contexto de la base de datos
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                try
                {
                    Log.Logger.Information("Aplicando Migraciones..");
                    dbContext.Database.Migrate();
                    Log.Logger.Information("Migraciones aplicadas con exito!");
                    SeedData.Initialize(dbContext);
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex.Message);
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices((_, services) =>
            {
                // Configurar el contexto de la base de datos
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                "Server=localhost,1433;Database=AtmDB;User=sa;Password=Password;MultipleActiveResultSets=true;TrustServerCertificate=true",
                options => options.EnableRetryOnFailure(
                    maxRetryCount: 5,  // Número máximo de intentos de conexión
                    maxRetryDelay: TimeSpan.FromSeconds(30),  // Retraso máximo entre intentos
                    errorNumbersToAdd: null  // Números de error adicionales para los que se debe reintentar la conexión                   
                )));
            });
    }

    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Verificar si ya existen datos en la base de datos
            if (context.Cards.Any())
            {
                Log.Logger.Information("Ya existen datos en la base");
                return;   
            }
            Log.Logger.Information("Agregando Datos iniciales..");
            var utcNow = DateTime.UtcNow;
            var transactions1 = new List<Transaction>()
            {
                new()
                {
                      Amount = 50.55M,
                     CreatedBy = "admin",
                     Type = TransactionType.Deposit,
                     LastModifiedBy = "admin",
                     CreatedAt = utcNow,
                     LastModified = utcNow
                },
                new()
                {
                      Amount = 44.55M,
                     CreatedBy = "admin",
                     Type = TransactionType.Extraction,
                     LastModifiedBy = "admin",
                     CreatedAt = utcNow,
                     LastModified = utcNow
                }
            };
            var transactions2 = new List<Transaction>()
            {
                new()
                {
                      Amount = 130.00M,
                     CreatedBy = "admin",
                     Type = TransactionType.Deposit,
                     LastModifiedBy = "admin",
                     CreatedAt = utcNow,
                     LastModified = utcNow
                },
                new()
                {
                      Amount = 100.00M,
                     CreatedBy = "admin",
                     Type = TransactionType.Extraction,
                     LastModifiedBy = "admin",
                     CreatedAt = utcNow,
                     LastModified = utcNow
                },
               new()
                {
                      Amount = 10000.00M,
                     CreatedBy = "admin",
                     Type = TransactionType.Deposit,
                     LastModifiedBy = "admin",
                     CreatedAt = utcNow,
                     LastModified = utcNow
                },
            };
            var card1 = new Card
            {
                CardNumber = "4970110000000062",
                OwnerName = "Juan Perez",
                Pin = 1234,
                AccountNumber = "01865281110786583688",
                IsBlocked = false,
                Balance =transactions1.Sum(x => x.Type == TransactionType.Deposit ? x.Amount : -x.Amount),
                CreatedBy = "admin",
                LastModifiedBy = "admin",
                Transactions = transactions1,
                CreatedAt = utcNow,
                LastModified = utcNow

            };
            var card2 = new Card
            {
                CardNumber = "3623000000001912",
                OwnerName = "Maria Gonzlez",
                Pin = 5678,
                AccountNumber = "31831767132372861697",
                IsBlocked = false,
                Balance = transactions2.Sum(x => x.Type == TransactionType.Deposit ? x.Amount : -x.Amount),
                CreatedBy = "admin",
                LastModifiedBy = "admin",
                Transactions = transactions2,
                CreatedAt = utcNow,
                LastModified = utcNow
            };

            context.Transactions.AddRange(transactions1);
            context.Transactions.AddRange(transactions2);
            context.Cards.AddRange(card1, card2);
            context.SaveChanges();
            Log.Logger.Information("Se agregaron con exitos datos iniciales");
        }
    }


}
