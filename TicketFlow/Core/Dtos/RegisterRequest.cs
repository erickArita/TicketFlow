namespace TicketFlow.Core.Dtos;

public record RegisterRequest(string Email, string Password, string UserName);