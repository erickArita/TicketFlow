namespace TicketFlow.Core.Ticket.Dtos;

public record ArchivoTicketResponse
{
    public Guid Id { get; set; }
    public string Link { get; set; }
}