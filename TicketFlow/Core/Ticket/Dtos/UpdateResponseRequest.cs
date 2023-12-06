namespace TicketFlow.Core.Ticket.Dtos;

public record UpdateResponseRequest(string Comentario, List<Guid> FilesIds);