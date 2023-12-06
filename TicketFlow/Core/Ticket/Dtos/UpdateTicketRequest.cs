namespace TicketFlow.Core.Ticket.Dtos;

public record UpdateTicketRequest : CreateTicketRequest
{
    public Guid EstadoId { get; init; }
}