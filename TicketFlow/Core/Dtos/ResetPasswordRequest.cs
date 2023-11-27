namespace TicketFlow.Core.Dtos;

public record ResetPasswordRequest(string Email, string Password, string Token);