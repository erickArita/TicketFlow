using TicketFlow.Core.User.Dtos;

namespace TicketFlow.Core.User;

public interface IUserService
{
    Task<bool> SetRoleAsync(SetRoleRequest setRoleRequest);
    Task<bool> UpdateRoleAsync(UpdateRoleRequest updateRoleRequest);
    Task<List<GetRoleRequest>> GetRolesAsync();
}