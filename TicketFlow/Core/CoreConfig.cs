using TicketFlow.Core.Authentication;
using TicketFlow.Core.Customer;

namespace TicketFlow.Core;

public static class CoreConfig
{
    public static void ConfigureCore(this IServiceCollection services)
    {
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<ICustomerService, CustomerService>();
    }
}