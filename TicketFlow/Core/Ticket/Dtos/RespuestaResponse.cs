namespace TicketFlow.Core.Ticket.Dtos;

public record RespuestaResponse
{
    public Guid Id { get; set; }
    public string Comentario { get; set; }
    public DateTime FechaCreacion { get; set; }
    public Guid TicketId { get; set; }
    public Guid? RespuestaPadreId { get; set; }
    public List<ArchivoTicketResponse> ArchivoRespuestas { get; set; }
    public string UsuarioNombre { get; set; }
    public string UsuarioId { get; set; }
    public List<RespuestaResponse> RespuestasHijas { get; set; }
    public bool modificado { get; set; }
    public DateTime? FechaModificacion { get; set; }
}