using System.ComponentModel.DataAnnotations;

namespace TicketFlow.Core.Ticket.Dtos;

public record CreateResponseRequest
{
    public List<Guid>? FilesIds { get; set; }

    [Required(ErrorMessage = "Comentario es requerido")]
    public string Comentario { get; set; }

    public Guid? RespuestaPadreId { get; set; }
}