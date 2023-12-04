using System.ComponentModel.DataAnnotations;

namespace TicketFlow.Core.Prioridad.Dtos;

public record CreatePrioridadRequest
{
    [Display(Name = "Nombre de la prioridad")]
    [Required(ErrorMessage = "El {0} es requerido")]
    public string Descripcion { get; set; }
}