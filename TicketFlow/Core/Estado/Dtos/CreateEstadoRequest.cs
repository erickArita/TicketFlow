using System.ComponentModel.DataAnnotations;

namespace TicketFlow.Core.Estado.Dtos;

public record CreateEstadoRequest
{
    [Display(Name = "Nombre del estado")]
    [Required(ErrorMessage = "El {0} es requerido")]
    public string Descripcion { get; set; }
}