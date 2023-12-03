using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Common.Exceptions;
using TicketFlow.Core.User.Dtos;

namespace TicketFlow.Core.User;

public class UserService : IUserService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityUser> _roleManager;

    public UserService(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityUser> roleManager
        )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    //metodo para setear el rol de un usuario
    public async Task<bool> SetRoleAsync(SetRoleRequest setRoleRequest)
    {
        var user = await _userManager.FindByIdAsync(setRoleRequest.UserId);
        if (user == null)
        {
            throw new TicketFlowException($"El usuario con id {setRoleRequest.UserId} no existe");
        }

        var role = await _roleManager.FindByNameAsync(setRoleRequest.Role);
        if (role == null)
        {
            throw new TicketFlowException($"El rol {setRoleRequest.Role} no existe");
        }

        var result = await _userManager.AddToRoleAsync(user, setRoleRequest.Role);
        
        return result.Succeeded;
    }
    
    //metodo para actualizar el rol de un usuario
    public async Task<bool> UpdateRoleAsync(UpdateRoleRequest updateRoleRequest)
    {
        var user = await _userManager.FindByIdAsync(updateRoleRequest.UserId);
        if (user == null)
        {
            throw new TicketFlowException($"El usuario con id {updateRoleRequest.UserId} no existe");
        }

        var role = await _roleManager.FindByNameAsync(updateRoleRequest.NewRoleName);
        if (role == null)
        {
            throw new TicketFlowException($"El rol {updateRoleRequest.NewRoleName} no existe");
        }

        var roleViejo = await _userManager.GetRolesAsync(user);
        
        var result = await _userManager.RemoveFromRoleAsync(user, roleViejo.First());
        if (!result.Succeeded)
        {
            throw new TicketFlowException(result.Errors.First().Description);
        }
        
        result = await _userManager.AddToRoleAsync(user, updateRoleRequest.NewRoleName);
        
        return result.Succeeded;
    }
    
    //metodo para eliminar el rol de un usuario
    /*public async Task<bool> RemoveRoleAsync(RemoveRoleRequest removeRoleRequest)
    {
        var user = await _userManager.FindByIdAsync(removeRoleRequest.UserId);
        if (user == null)
        {
            throw new TicketFlowException($"El usuario con id {removeRoleRequest.UserId} no existe");
        }

        var role = await _roleManager.FindByNameAsync(removeRoleRequest.Role);
        if (role == null)
        {
            throw new TicketFlowException($"El rol {removeRoleRequest.Role} no existe");
        }

        var result = await _userManager.RemoveFromRoleAsync(user, removeRoleRequest.Role);
        
        return result.Succeeded;
    }*/
    
    //metodo para obtener los usuarios y sus roles
    public async Task<List<GetRoleRequest>> GetUsersWithRolesAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var usersWithRoles = new List<GetRoleRequest>();

        foreach (var user in users)
        {
            var roles = await _roleManager.FindByNameAsync(user.);
            usersWithRoles.Add(new GetRoleRequest(user.Id, user.UserName, user.Email, roles));
        }

        return usersWithRoles;
    }
}