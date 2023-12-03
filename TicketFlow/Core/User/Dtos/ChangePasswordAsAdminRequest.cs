namespace TicketFlow.Core.Dtos;

public record ChangePasswordAsAdminRequest(string TargetUserId, string NewPassword);