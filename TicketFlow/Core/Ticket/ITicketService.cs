using TicketFlow.Controllers;
using TicketFlow.Core.Ticket.Dtos;

namespace TicketFlow.Core.Ticket;

public interface ITicketService
{
    public Task<IReadOnlyCollection<TicketResponse>> GetAllAsync();
    public Task<TicketWithHistoryResponse> GetByIdAsync(Guid? id);
    public Task<TicketResponse> AddAsync(CreateTicketRequest ticketCreationDto);
    public Task<TicketResponse> UpdateAsync(Guid id, UpdateTicketRequest ticketUpdateDto);
    public Task DeleteAsync(Guid id);
    public Task<TicketResponse> AddUser2TicketAsync(Guid ticketId, Guid userId);
    public Task<TicketResponse> UpdateStatusAsync(Guid ticketId, Guid estadoId);
    public Task<TicketResponse> UpdatePriorityAsync(Guid ticketId, Guid prioridadId);
}