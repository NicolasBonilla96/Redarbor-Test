using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Redarbor.Domain.Entities.Auth;
using Redarbor.Persistence.Contexts;
using Redarbor.Persistence.Database.Seeds;

namespace Redarbor.Persistence.Extensions;

public class DbContextInitializer(
        ILogger<DbContextInitializer> logger,
        RedarborDbContext db,
        UserManager<User> userManager,
        RoleManager<Role> roleManager
    )
{
    public async Task InitializeAsync()
    {
        try
        {
            var pending = await db.Database.GetPendingMigrationsAsync();
            if (pending.Any())
            {
                logger.LogInformation($"Pending migrations: {string.Join(", ", pending)}.");
                await db.Database.MigrateAsync();
            }
            else
            {
                logger.LogInformation("There's no pending migrations to apply.");
            }
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        RedarborDbSeeder redarborDbSeeder = new(db);
        await redarborDbSeeder.SeedUsers(userManager, roleManager);
    }
}
