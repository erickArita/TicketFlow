using Microsoft.AspNetCore.Identity;
using TicketFlow;
using TicketFlow.DB.Seeders;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

using (var scope = app.Services.CreateScope())//necesario para usar el identity
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();
    
    try
    {
        var userManager = service.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
        
        await UsersRolesSeeder.LoadDataAsync(userManager, roleManager, loggerFactory);
    }
    catch (Exception e)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "Ocurrió un error al migrar o al insertar los datos");
    }
}

app.Run();