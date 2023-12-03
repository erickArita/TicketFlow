using System.ComponentModel.DataAnnotations;

namespace TicketFlow.Core.Authentication.Dtos;

public record ResetPasswordRequest
{
    [Required(ErrorMessage = "El email es requerido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "La nueva contraseña es requerida")]
    public string Password { get; set; }

    [Required(ErrorMessage = "El token es requerido")]
    public string Token { get; set; }
}