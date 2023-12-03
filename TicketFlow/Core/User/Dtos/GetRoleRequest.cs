namespace TicketFlow.Core.User.Dtos;

public record GetRoleRequest(string UserId, string UserName, string UserEmail, string Role);