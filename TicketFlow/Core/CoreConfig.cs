using TicketFlow.Core.Authentication;

namespace TicketFlow.Core;

public static class CoreConfig
{
    public static void ConfigureCore(this IServiceCollection services)
    {
        services.AddTransient<IAuthenticationService, AuthenticationService>();
    }
}