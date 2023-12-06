using TicketFlow.DB.Contexts;
using TicketFlow.Entities;

namespace TicketFlow.DB.Seeders;

public static class CustomersSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.Clientes.Any())
            {
                var firstCliente = new Cliente
                {
                    Id = Guid.NewGuid(),
                    Apellido = "Perez",
                    Correo = "perez@gmail.com",
                    Nombre = "Juan",
                    Telefono = "1234567890",
                };
                context.Clientes.Add(firstCliente);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
            logger.LogError(e.Message);
            throw;
        }
    }
}