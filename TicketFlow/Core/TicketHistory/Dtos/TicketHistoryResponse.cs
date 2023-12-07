namespace TicketFlow.Core.TicketHistory.Dtos;

public record TicketHistoryResponse
{
    public Guid Id { get; set; }

    public string Descripcion { get; set; }

    public Guid UsuarioId { get; set; }

    public Guid TicketId { get; set; }

    public string UsuarioNombre { get; set; }
    
    public DateTime? FechaActualizacion { get; set; }
    
    public DateTime FechaCreacion { get; set; }
}