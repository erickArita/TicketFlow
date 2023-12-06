using TicketFlow.DB.Contexts;
using TicketFlow.Entities;
using TicketFlow.Entities.Enums;

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
                        Descripcion = Estados.Pendiente
                    },
                    new Estado
                    {
                        Id = Guid.NewGuid(),
                        Descripcion = Estados.Procesando
                    },
                    new Estado
                    {
                        Id = Guid.NewGuid(),
                        Descripcion = Estados.Terminado
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