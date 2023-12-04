using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Utils;
using TicketFlow.Core.Authentication;
using TicketFlow.Core.Dtos;
using TicketFlow.Core.User;
using TicketFlow.Entities.Enums;

namespace TicketFlow.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    //endpoint para listar los usuarios con sus roles
    [HttpGet]
    [Route("getUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();

        return Ok(new AplicationResponse<IEnumerable<UserRoleResponse>>
        {
            Message = "Usuarios listados correctamente",
            Data = users
        });
    }

    //endpoint para actualizar el rol de un usuario
    [HttpPatch]
    [Route("updateRole")]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequest updateRoleRequestApiModel)
    {
        await _userService.UpdateRoleAsync(updateRoleRequestApiModel);

        return Ok(new AplicationResponse<string>
        {
            Message = "Rol actualizado correctamente",
            Data = null
        });
    }
    
    //endpoint para cambiar contraseña como admin
    [HttpPatch]
    [Route("changePasswordAsAmin")]
    public async Task<IActionResult> ChangePasswordAsAdmin([FromBody] ChangePasswordAsAdminRequest changePasswordAsAdminRequest)
    {
        await _userService.ChangePasswordAsAdminAsync(changePasswordAsAdminRequest);

        return Ok(new AplicationResponse<string>
        {
            Message = "Contraseña actualizada correctamente",
            Data = null
        });
    }
}
