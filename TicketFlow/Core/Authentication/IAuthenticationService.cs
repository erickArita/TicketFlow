using TicketFlow.Core.Dtos;

namespace TicketFlow.Core.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(RegisterRequest registerRequest);
    Task<string> LoginAsync(LoginRequest loginRequest);
    Task<bool> ResetPasswordEmail(string email);
    Task<bool> ResetPassword(ResetPasswordRequest resetPasswordRequest);
}