using System.ComponentModel.DataAnnotations;

namespace TicketFlow.Core.Ticket.Dtos;

public record CreateTicketRequest
{
    [Required(ErrorMessage = "Asunto es requerido")]
    public string Asunto { get; set; }

    public string Descripcion { get; set; }

    [Required(ErrorMessage = "Cliente es requerido")]
    public Guid ClienteId { get; set; }

    [Required(ErrorMessage = "Prioridad es requerida")]
    public Guid PrioridadId { get; set; }

    [Required(ErrorMessage = "Usuario asignado es requerido")]
    public string UsuarioAsignadoId { get; set; }

    [Required(ErrorMessage = "Fecha de vencimiento es requerida")]
    public DateTime FechaVencimiento { get; set; }

    public List<Guid>? ArchivosTickets { get; set; } = new();
}