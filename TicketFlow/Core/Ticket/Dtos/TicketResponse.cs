namespace TicketFlow.Core.Ticket.Dtos;

public record TicketResponse
{
    public Guid Id { get; set; }
    public string Asunto { get; set; }
    public string Descripcion { get; set; }
    public string ClienteNombre { get; set; }
    public Guid ClienteId { get; set; }
    public string PrioridadDescripcion { get; set; }
    public Guid PrioridadId { get; set; }
    public string UsuarioAsignadoNombre { get; set; }
    public string UsuarioAsignadoId { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public string EstadoDescripcion { get; set; }
    public Guid EstadoId { get; set; }
    public List<ArchivoTicketResponse> ArchivosTickets { get; set; }
}