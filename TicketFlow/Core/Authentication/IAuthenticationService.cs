using TicketFlow.Core.Authentication.Dtos;
using TicketFlow.Core.Dtos;

namespace TicketFlow.Core.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(RegisterRequest registerRequest);
    Task<string> LoginAsync(LoginRequest loginRequest);
    Task<bool> ResetPasswordRequest(string email);
    Task<bool> ResetPassword(ResetPasswordRequest resetPasswordRequest);
}