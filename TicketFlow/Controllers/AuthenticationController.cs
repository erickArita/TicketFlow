using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Utils;
using TicketFlow.Core.Authentication;
using TicketFlow.Core.Dtos;
using TicketFlow.Helpers;
using TicketFlow.Services.Email;

namespace TicketFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequestApiModel)
    {
        string token = await _authenticationService.LoginAsync(loginRequestApiModel);

        return Ok(new AplicationResponse<string>
        {
            Message = "Login exitoso",
            Data = token
        });
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequestApiModel)
    {
        string token = await _authenticationService.RegisterAsync(registerRequestApiModel);

        return Ok(new AplicationResponse<string>
        {
            Message = "Registro exitoso",
            Data = token
        });
    }
    
    [HttpPost]
    [Route("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] string NewPassword, [FromQuery] string Email, [FromQuery] string Token)
    {
        var resetPasswordRequest = new ResetPasswordRequest
        (
            Email : Email,
            Password : NewPassword,
            Token : Token
        );
        await _authenticationService.ResetPassword(resetPasswordRequest);

        return Ok(new AplicationResponse<string>
        {
            Message = "Cambio de contraseña exitoso 😎",
            Data = null
        });
    }
}