using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketFlow;
using TicketFlow.DB.Contexts;
using TicketFlow.DB.Seeders;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

using var scope1 = app.Services.CreateScope();
var dataContext = scope1.ServiceProvider.GetRequiredService<ApplicationDbContext>();
dataContext.Database.Migrate();

startup.Configure(app, app.Environment);

using var scope2 = app.Services.CreateScope();
var service = scope2.ServiceProvider;
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


app.Run();