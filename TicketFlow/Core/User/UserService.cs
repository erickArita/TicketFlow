using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Common.Exceptions;
using TicketFlow.Core.Dtos;

namespace TicketFlow.Core.User;

public class UserService : IUserService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager
    )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    //metodo para que actualice el rol de un usuario
    public async Task<bool> UpdateRoleAsync(UpdateRoleRequest updateRoleRequest)
    {
        var user = await _userManager.FindByIdAsync(updateRoleRequest.UserId);
        if (user == null)
        {
            throw new TicketFlowException($"El usuario con id {updateRoleRequest.UserId} no existe ❌😡");
        }
        
        //verrificar que el rol exista
        var existRole = await _roleManager.FindByNameAsync(updateRoleRequest.NewRoleName);
        if (existRole == null)
        {
            throw new TicketFlowException($"El rol {updateRoleRequest.NewRoleName} no existe ❌😡");
        }

        var role = await _userManager.GetRolesAsync(user);
        var result = await _userManager.RemoveFromRoleAsync(user, role[0]);
        if (!result.Succeeded)
        {
            throw new TicketFlowException(result.Errors);
        }

        var result2 = await _userManager.AddToRoleAsync(user, updateRoleRequest.NewRoleName);
        if (!result2.Succeeded)
        {
            throw new TicketFlowException(result2.Errors);
        }

        return result.Succeeded;
    }
    
    //metodo para listar los usuarios con sus roles
    public async Task<List<UserRoleResponse>> GetUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var usersRoleResponse = new List<UserRoleResponse>();
        foreach (var user in users)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var userRoleResponse =
                new UserRoleResponse(user.Id, user.UserName, user.Email, string.Join(",", userRoles));
            usersRoleResponse.Add(userRoleResponse);
        }

        return usersRoleResponse;
    }
}