using TicketFlow.Core.Ticket.Dtos;

namespace TicketFlow.Core.Ticket.Dtos;

public record TicketWithResponses : TicketResponse
{
    public List<RespuestaResponse> Respuestas { get; set; }
}