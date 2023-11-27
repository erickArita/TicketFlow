using Microsoft.AspNetCore.Identity;
using TicketFlow.DB.Contexts;
using TicketFlow.Entities.Enums;

namespace TicketFlow.DB.Seeders;

public static class UsersRolesSeeder
{
    public static async Task LoadDataAsync(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ILoggerFactory loggerFactory)
    {
        try
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
                await roleManager.CreateAsync(new IdentityRole(Roles.Staff));
            }

            if (!userManager.Users.Any())
            {
                var userAdmin = new IdentityUser
                {
                    UserName = "jperez",
                    Email = "jperez@me.com"
                };
                await userManager.CreateAsync(userAdmin, "Temporal01#");
                await userManager.AddToRoleAsync(userAdmin, Roles.Admin);

                var normalUser = new IdentityUser
                {
                    UserName = "pepito@me.com",
                    Email = "pepito@me.com"
                };
                await userManager.CreateAsync(normalUser, "Temporal01#");
                await userManager.AddToRoleAsync(normalUser, Roles.Staff);
            }
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
            logger.LogError(e.Message);
            throw;
        }
    }
}