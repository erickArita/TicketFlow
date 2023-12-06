namespace TicketFlow.Core.TicketHistory;

public interface ITicketHistoryService
{
    Task AddTicketHistoryAsync(string descripcion, Guid ticketId);
}