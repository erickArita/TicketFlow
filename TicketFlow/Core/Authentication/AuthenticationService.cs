using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TicketFlow.Common.Exceptions;
using TicketFlow.Common.Utils;
using TicketFlow.Core.Dtos;
using TicketFlow.Entities.Enums;
using TicketFlow.Helpers;
using TicketFlow.Services.Email;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TicketFlow.Core.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _config;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailSenderService _emailSenderService;

    public AuthenticationService(
        IConfiguration config,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IEmailSenderService emailSenderService
    )
    {
        _config = config;
        _signInManager = signInManager;
        _userManager = userManager;
        _emailSenderService = emailSenderService;
    }

    public async Task<string> RegisterAsync(RegisterRequest registerRequest)
    {
        var existUser = await _userManager.FindByEmailAsync(registerRequest.Email);
        var existUserName = await _userManager.FindByNameAsync(registerRequest.UserName);

        if (existUserName != null)
        {
            throw new TicketFlowException($"El usuario con nombre {registerRequest.UserName} ya existe");
        }

        if (existUser != null)
        {
            throw new TicketFlowException($"El usuario con email {registerRequest.Email} ya existe");
        }

        var user = new IdentityUser
        {
            UserName = registerRequest.UserName,
            Email = registerRequest.Email
        };

        var result = await _userManager.CreateAsync(user, registerRequest.Password);

        if (!result.Succeeded)
        {
            throw new TicketFlowException(result.Errors.First().Description);
        }

        await _userManager.AddToRoleAsync(user, Roles.Staff);

        var token = await GenerateAccessToken(user);

        await _emailSenderService.SendEmailAsync(registerRequest.Email, "Registro",
            EmailTemplates.RegisterTemplate("Bienvenido a TicketFlow", "Se ha registrado correctamente"));

        return token;
    }

    public async Task<string> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByNameAsync(loginRequest.UserName);

        if (user == null)
        {
            throw new TicketFlowException($"El usuario con nombre {loginRequest.UserName} no existe  🔑❌😡");
        }

        var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, false, false);

        if (!result.Succeeded)
        {
            throw new TicketFlowException($"Error al iniciar sesión con el usuario {loginRequest.UserName} 🔑❌😡");
        }

        var token = await GenerateAccessToken(user);

        await _emailSenderService.SendEmailAsync(user.Email, "Inicio de sesión", EmailTemplates.LoginTemplate());
        return token;
    }

    public async Task<bool> ResetPasswordEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            throw new TicketFlowException($"El usuario con email {email} no existe 🔑❌😡");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var backendUrl = _config["WEBSITE_URL"];
        var resetPasswordUrl = $"{backendUrl}/reset-password?token={token}&email={email}";
        var emailBody =
            $"Para restablecer su contraseña, haga clic en el siguiente enlace: <a href='{resetPasswordUrl}'>Restablecer contraseña</a>";

        return await _emailSenderService.SendEmailAsync(user.Email, "Autenticacion",
            EmailTemplates.ResetPasswordTemplate("Cambiar contraseña", emailBody));
    }

    public async Task<bool> ResetPassword(ResetPasswordRequest resetPasswordRequest)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordRequest.Email);
        if (user == null)
        {
            throw new TicketFlowException($"El usuario con email {resetPasswordRequest.Email} no existe 🔑❌😡");
        }

        var result =
            await _userManager.ResetPasswordAsync(user, resetPasswordRequest.Token, resetPasswordRequest.Password);

        if (!result.Succeeded)
        {
            throw new TicketFlowException(result.Errors);
        }

        var emailBody = "Su contraseña ha sido cambiada correctamente";

        await _emailSenderService.SendEmailAsync(user.Email, "Cambio de contraseña",
            EmailTemplates.ChangePasswordTemplate("Cambio de contraseña", emailBody));

        return result.Succeeded;
    }

    private async Task<string> GenerateAccessToken(IdentityUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, $"{user.UserName}"),
            new(ClaimTypes.Email, user?.Email ?? string.Empty),
        };

        claims.AddRange(userRoles.Select(userRole => new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole)));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"] ?? string.Empty));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(
            issuer: _config["Jwt:ValidIssuer"],
            audience: _config["Jwt:ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}