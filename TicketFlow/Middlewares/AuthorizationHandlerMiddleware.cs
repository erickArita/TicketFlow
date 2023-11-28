using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using TicketFlow.Common.Exceptions;

namespace TicketFlow.Middlewares;

public class AuthorizationHandlerMiddleware : IAuthorizationMiddlewareResultHandler
{
    public Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Challenged)
        {
            throw new UnauthorizedException("No se encuentra autenticado o el token es inválido ❌🔑👮🚨");
        }

        if (authorizeResult.Forbidden)
        {
            throw new ForbiddenException("No tiene permisos para realizar esta acción ❌👮🚓🚨 ");
        }

        return next(context);
    }
}