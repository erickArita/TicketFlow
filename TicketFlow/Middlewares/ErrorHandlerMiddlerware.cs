using System.Net;
using TicketFlow.Common.Exceptions;
using TicketFlow.Common.Utils;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TicketFlow.Middlewares;

public class ErrorHandlerMiddlerware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddlerware> _logger;

    public ErrorHandlerMiddlerware(RequestDelegate next, ILogger<ErrorHandlerMiddlerware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception, _logger);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
    {
        ExceptionResponse response = new();
        switch (exception)
        {
            case TicketFlowException ticketFlowException:
                logger.LogInformation(exception, "----------------------CONTAWEB ERROR TYPE---------------------");
                response.Errors = ticketFlowException.Errors ?? string.Empty;
                response.StatusCode = HttpStatusCode.BadRequest;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;

            case NotFoundException notFoundException:
                logger.LogError(exception, "----------------------NOT FOUND ERROR TYPE---------------------");
                response.Errors = notFoundException.Errors ?? string.Empty;
                response.StatusCode = HttpStatusCode.NotFound;
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;

            case UnauthorizedException unauthorizedException:
                logger.LogInformation(exception, "----------------------AUTHENTICATION EXCEPTION---------------------");
                response.Errors = unauthorizedException.Errors ?? string.Empty;
                response.StatusCode = HttpStatusCode.Unauthorized;
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;

            case ForbiddenException forbiddenException:
                logger.LogError(exception, "----------------------AUTHORIZATION EXCEPTION---------------------");
                response.Errors = forbiddenException.Message ?? string.Empty;
                response.StatusCode = HttpStatusCode.Unauthorized;
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;


            case { }:
                logger.LogError(exception, "----------------------SERVER ERROR TYPE---------------------");
                response.Errors = "Ha ocurrido un error en el servidor";
                response.StatusCode = HttpStatusCode.InternalServerError;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        context.Response.ContentType = "application/json";

        var result = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(result);
    }
}