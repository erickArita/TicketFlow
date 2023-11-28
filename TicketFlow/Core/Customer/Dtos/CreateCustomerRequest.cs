using System.ComponentModel.DataAnnotations;

namespace TicketFlow.Core.Customer.Dtos;

public record CreateCustomerRequest
{
    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El $1 es requerido")]
    public string Nombre { get; set; }

    [Display(Name = "Apellido")]
    [Required(ErrorMessage = "El $1 es requerido")]
    public string Apellido { get; set; }

    [Display(Name = "Correo")]
    [Required(ErrorMessage = "El $1 es requerido")]
    [DataType(DataType.EmailAddress)]
    public string Correo { get; set; }

    [DataType(DataType.PhoneNumber)] public string Telefono { get; set; }
}