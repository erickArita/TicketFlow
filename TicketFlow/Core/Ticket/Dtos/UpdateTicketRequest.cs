using System.ComponentModel.DataAnnotations;

namespace TicketFlow.Core.Ticket.Dtos;

public record UpdateTicketRequest : CreateTicketRequest
{
    [Required(ErrorMessage = "El estado no puede ser nulo")]
    public Guid EstadoId { get; init; }
}