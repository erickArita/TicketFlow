using TicketFlow.Core.Prioridad.Dtos;

namespace TicketFlow.Core.Prioridad;

public interface IPrioridadService
{
    Task<PrioridadResponse> AddSync(CreatePrioridadRequest createPrioridadRequest);
    Task<PrioridadResponse> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<PrioridadResponse>> GetAllAsync();
    Task UpdateAsync(UpdatePrioridadRequest updatePrioridadRequest, Guid id);
    Task DeleteAsync(Guid id);
}