using TicketFlow.DB.Contexts;
using TicketFlow.Entities;

namespace TicketFlow.DB.Seeders;

public static class EstadosSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.Estados.Any())
            {
                var estados = new List<Estado>
                {
                    new Estado
                    {
                        Id = Guid.NewGuid(),
                        Descripcion = "Nuevo"
                    },
                    new Estado
                    {
                        Id = Guid.NewGuid(),
                        Descripcion = "Pendiente"
                    },
                    new Estado
                    {
                        Id = Guid.NewGuid(),
                        Descripcion = "Terminado"
                    }
                };

                await context.Estados.AddRangeAsync(estados);
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