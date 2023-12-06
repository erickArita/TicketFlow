using TicketFlow.Core.Ticket.Dtos;

namespace TicketFlow.Core.Respuestas;

public interface IRespuestasService
{
    Task<RespuestaResponse> AddResponseAsync(CreateResponseRequest respuestaCreationDto);
    Task<RespuestaResponse> UpdateResponseAsync(Guid respuestaId, UpdateResponseRequest respuestaUpdateDto);
}