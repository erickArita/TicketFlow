using System.ComponentModel.DataAnnotations;

namespace TicketFlow.Core.Ticket.Dtos;

public record CreateTicketRequest
{
    [Required(ErrorMessage = "Asunto es requerido")]
    public string Asunto { get; init; }

    public string Descripcion { get; init; }

    [Required(ErrorMessage = "Cliente es requerido")]
    public Guid ClienteId { get; init; }

    [Required(ErrorMessage = "Prioridad es requerida")]
    public Guid PrioridadId { get; init; }

    [Required(ErrorMessage = "Usuario asignado es requerido")]
    public string UsuarioAsignadoId { get; init; }

    [Required(ErrorMessage = "Fecha de vencimiento es requerida")]
    public DateTime FechaVencimiento { get; init; }

    public List<Guid>? ArchivosIds { get; set; } = new();
}