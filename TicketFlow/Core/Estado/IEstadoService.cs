using TicketFlow.Core.Estado.Dtos;

namespace TicketFlow.Core.Estado;

public interface IEstadoService
{
    Task<EstadoResponse> AddSync(CreateEstadoRequest createEstadoRequest);
    Task<EstadoResponse> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<EstadoResponse>> GetAllAsync();
    Task UpdateAsync(UpdateEstadoRequest updateEstadoRequest, Guid id);
    Task DeleteAsync(Guid id);
    Task<EstadoResponse> GetByNameAsync(string name);
}