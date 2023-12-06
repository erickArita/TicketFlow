using TicketFlow.Core.ArchivoAdjunto;
using TicketFlow.Core.Authentication;
using TicketFlow.Core.Customer;
using TicketFlow.Core.Estado;
using TicketFlow.Core.Prioridad;
using TicketFlow.Core.Respuestas;
using TicketFlow.Core.Ticket;
using TicketFlow.Core.TicketHistory;
using TicketFlow.Core.User;

namespace TicketFlow.Core;

public static class CoreConfig
{
    public static void ConfigureCore(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<IPrioridadService, PrioridadService>();
        services.AddTransient<IEstadoService, EstadoService>();
        services.AddTransient<ITicketService, TicketService>();
        services.AddTransient<IArchivoAdjuntoService, ArchivoAdjuntoService>();
        services.AddTransient<ITicketHistoryService, TicketHistoryService>();
        services.AddTransient<IRespuestasService, RespuestasService>();
    }
}