using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Utils;
using TicketFlow.Core.Authentication;
using TicketFlow.Core.Authentication.Dtos;
using TicketFlow.Core.Dtos;

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
    [Route("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePassword request, [FromQuery] string Email,
        [FromQuery] string Token)
    {
        var resetPasswordRequest = new ResetPasswordRequest
        {
            Email = Email,
            Password = request.NewPassword,
            Token = Token
        };
        await _authenticationService.ResetPassword(resetPasswordRequest);

        return Ok(new AplicationResponse<string>
        {
            Message = "Cambio de contraseña exitoso 😎",
            Data = null
        });
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
    [Route("reset-password-request")]
    public async Task<IActionResult> ResetPasswordRequest([FromBody] string email)
    {
        var token = await _authenticationService.ResetPasswordRequest(email);

        return Ok(new AplicationResponse<string>
        {
            Message = "Se ha enviado un correo para restablecer la contraseña",
            Data = token
        });
    }
}