using TicketFlow.DB.Contexts;
using TicketFlow.Entities;

namespace TicketFlow.DB.Seeders;

public static class PrioridadesSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.Prioridades.Any())
            {
                var prioridades = new List<Prioridad>
                {
                    new Prioridad
                    {
                        Id = Guid.NewGuid(),
                        Descripcion = "Critica"
                    },
                    new Prioridad
                    {
                        Id = Guid.NewGuid(),
                        Descripcion = "Importante"
                    },
                    new Prioridad
                    {
                        Id = Guid.NewGuid(),
                        Descripcion = "Menor"
                    }
                };

                await context.Prioridades.AddRangeAsync(prioridades);
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