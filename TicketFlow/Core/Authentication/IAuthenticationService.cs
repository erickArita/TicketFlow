using TicketFlow.Core.Dtos;

namespace TicketFlow.Core.Authentication;

public interface IAuthenticationService
{
    Task<string> Register(RegisterRequest registerRequest);
    Task<string> Login(LoginRequest loginRequest);
    Task<bool> ResetPasswordEmail(string email);
    Task<bool> ResetPassword(ResetPasswordRequest resetPasswordRequest);
}