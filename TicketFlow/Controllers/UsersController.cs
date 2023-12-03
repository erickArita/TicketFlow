using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Core.User;
using TicketFlow.Core.User.Dtos;

namespace TicketFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        //endpoint para obtener el rol de un usuario
        [HttpGet]
        [Route("getRole")]
        public async Task<IActionResult> GetRole([FromBody] GetRoleRequest getRoleRequest)
        {
            var role = await _userService.GetRolesAsync();
            return Ok(role);
        }
        
        // endpoint para setear el rol de un usuario
        [HttpPost]
        [Route("setRole")]
        public async Task<IActionResult> SetRole([FromBody] SetRoleRequest setRoleRequest)
        {
            await _userService.SetRoleAsync(setRoleRequest);
            return Ok();
        }
        // endpoint para actualizar el rol de un usuario
        [HttpPost]
        [Route("updateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequest updateRoleRequest)
        {
            await _userService.UpdateRoleAsync(updateRoleRequest);
            return Ok();
        }
    }
}
