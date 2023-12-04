using TicketFlow.Core.Authentication;
using TicketFlow.Core.Customer;
using TicketFlow.Core.Prioridad;
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
    }
}