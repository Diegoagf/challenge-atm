using Challenge.Atm.Domain.EF.DBContexts;
using Challenge.Atm.Domain.Entities;
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
                "Server=localhost,1433;Database=MyDatabase;User=sa;Password=Password;MultipleActiveResultSets=true;TrustServerCertificate=true",
                options => options.EnableRetryOnFailure(
                    maxRetryCount: 5,  // Número máximo de intentos de conexión
                    maxRetryDelay: TimeSpan.FromSeconds(30),  // Retraso máximo entre intentos
                    errorNumbersToAdd: null  // Números de error adicionales para los que se debe reintentar la conexión                   
                )));
            });
    }

    // Clase para insertar datos de prueba en la base de datos
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Verificar si ya existen datos en la base de datos
            if (context.Users.Any() && context.Cards.Any())
            {
                Log.Logger.Information("Ya existen datos en la base");
                return;   // La base de datos ya contiene datos
            }
            Log.Logger.Information("Agregando Datos iniciales..");
            var card1 = new Card
            {
                CardNumber = "4970110000000062",
                Pin = 1234,
                AccountNumber = "01865281110786583688",
                IsBlocked = false,
                Balance = 0,
                CreatedBy = "admin",
                CreatedAt = DateTime.UtcNow,
                LastModifiedBy = "admin",
                LastModified = DateTime.UtcNow
            };
            var card2 = new Card
            {
                CardNumber = "36230000000019",
                Pin = 0001,
                AccountNumber = "31831767132372861697",
                IsBlocked = false,
                Balance = 0,
                CreatedBy = "admin",
                CreatedAt = DateTime.UtcNow,
                LastModifiedBy = "admin",
                LastModified = DateTime.UtcNow
            };
            var users = new List<User>
            {
                new User
                {
                    Name = "Juan Perez",
                    Rol = "Cliente",
                    CreatedBy = "admin",
                    CreatedAt = DateTime.UtcNow,
                    LastModifiedBy = "admin",
                    LastModified = DateTime.UtcNow,
                    Cards = new List<Card>
                    {
                        card1
                    }
                },
                new User
                {
                    Name = "Maria Gonzalez",
                    Rol = "Administrador",
                    CreatedBy = "admin",
                    CreatedAt = DateTime.UtcNow,
                    LastModifiedBy = "admin",
                    LastModified = DateTime.UtcNow,
                    Cards = new List<Card>
                    {
                        card2
                    }
                }
            };
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }


}
