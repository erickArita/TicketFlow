using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Common.Exceptions;
using TicketFlow.DB.Contexts;
using TicketFlow.Entities;
using TicketFlow.Entities.Enums;

namespace TicketFlow.Core.Ticket.Extensions;

public record ValidatedDate(Cliente Customer, Entities.Prioridad Prioridad, IdentityUser User);

public static class ValidateTicket
{
    public static async Task<ValidatedDate> ValidateAsync(this Entities.Ticket ticket,
        ApplicationDbContext dbContext
    )
    {
        if (ticket.ClienteId == Guid.Empty) throw new NotFoundException($"El cliente no puede ser nulo");
        var customer = await dbContext.Clientes.FirstOrDefaultAsync(c => c.Id == ticket.ClienteId)
                       ?? throw new NotFoundException($"Cliente con id {ticket.ClienteId} no existe");

        if (ticket.PrioridadId == Guid.Empty) throw new NotFoundException($"La prioridad no puede ser nula");
        var prioridad = await dbContext.Prioridades.FirstOrDefaultAsync(p => p.Id == ticket.PrioridadId) ??
                        throw new NotFoundException($"Prioridad con id {ticket.PrioridadId} no existe");
        
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == ticket.UsuarioId) ??
                   throw new NotFoundException($"Usuario con id {ticket.UsuarioId} no existe");
        
        return new ValidatedDate(customer, prioridad, user);
    }

    public static async Task ValidateUpdateAsync(this Entities.Ticket ticket,
        ApplicationDbContext dbContext
    )
    {
        await ticket.ValidateAsync(dbContext);

        if (ticket.EstadoId == Guid.Empty) throw new NotFoundException($"El estado no puede ser nulo");
        
        var existeEstado = await dbContext.Estados.AnyAsync(e => e.Id == ticket.EstadoId);

        if (!existeEstado) throw new NotFoundException($"Estado con id {ticket.EstadoId} no existe");
    }
}