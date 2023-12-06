using TicketFlow.Core.Ticket.Dtos;

namespace TicketFlow.Controllers;

public record TicketWithResponses : TicketResponse
{
    public List<RespuestaResponse> Respuestas { get; set; }
}