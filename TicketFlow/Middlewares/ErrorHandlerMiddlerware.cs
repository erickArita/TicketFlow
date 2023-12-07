using System.Net;
using TicketFlow.Common.Exceptions;
using TicketFlow.Common.Logger;
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

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception,
        ILogger<ErrorHandlerMiddlerware> logger)
    {
        ExceptionResponse response = new();
        var logState = new LogState();
        logState.OperationType = OperationTypes.Log;
        logState.exception = exception.ToString();
        switch (exception)
        {
            case TicketFlowException ticketFlowException:
                response.Errors = ticketFlowException.Errors ?? string.Empty;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Title = "----------------------TicketFlow ERROR TYPE---------------------";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;

            case NotFoundException notFoundException:
                response.Errors = notFoundException.Errors ?? string.Empty;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Title = "----------------------NOT FOUND ERROR TYPE---------------------";

                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;

            case UnauthorizedException unauthorizedException:
                response.Errors = unauthorizedException.Errors ?? string.Empty;
                response.StatusCode = HttpStatusCode.Unauthorized;
                response.Title = "----------------------AUTHENTICATION EXCEPTION---------------------";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;

            case ForbiddenException forbiddenException:
                response.Title = "----------------------AUTHORIZATION EXCEPTION---------------------";
                response.Errors = forbiddenException.Message ?? string.Empty;
                response.StatusCode = HttpStatusCode.Unauthorized;
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;


            case { }:
                response.Title = "---------------------SERVER ERROR TYPE---------------------";
                response.Errors = "Ha ocurrido un error en el servidor";
                response.StatusCode = HttpStatusCode.InternalServerError;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        logState.Title = response.Title;
        logState.exception = exception.ToString();
        var exp = response.Title + exception.Message;
        logger.LogError(exp, new[] { logState });
        context.Response.ContentType = "application/json";

        var result = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(result);
    }
}