using TicketFlow.Core.TicketHistory.Dtos;

namespace TicketFlow.Core.Ticket.Dtos;

public record TicketWithHistoryResponse : TicketWithResponses
{
    public List<TicketHistoryResponse> TiketsHistories { get; set; }
}