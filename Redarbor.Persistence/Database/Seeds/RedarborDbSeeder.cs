using Microsoft.AspNetCore.Identity;
using Redarbor.Domain.Entities.Auth;
using Redarbor.Persistence.Contexts;
using Redarbor.Persistence.Database.Seeds.Dtos;
using System.Text.Json;

namespace Redarbor.Persistence.Database.Seeds;

public class RedarborDbSeeder(
        RedarborDbContext db
    )
{
    private readonly RedarborDbContext _db = db;

    private static List<T> GetDefaultData<T>(string path) where T : class
    {
        var fileData = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<T>>(fileData);
    }

    public async Task SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        var usersSeed = GetDefaultData<UserSeed>(Path.Combine(AppContext.BaseDirectory, "Database/Seeds/JsonSeeds/Users.json"));
        foreach (var userSeed in usersSeed)
            if (await userManager.FindByNameAsync(userSeed.UserName!) is null && await userManager.FindByEmailAsync(userSeed.Email) is null)
            {
                var user = new User
                {
                    UserName = userSeed.UserName,
                    Email = userSeed.Email,
                    PhoneNumber = userSeed.PhoneNumber,
                };
                var respUser = await userManager.CreateAsync(user, userSeed.Password);
                var role = await roleManager.FindByNameAsync(userSeed.Role);
                var respRole = new IdentityResult();
                if (role is null)
                {
                    var _role = new Role
                    {
                        Name = userSeed.Role,
                        Description = userSeed.DescriptionRole
                    };
                    await roleManager.CreateAsync(_role);
                }
                await userManager.AddToRoleAsync(user, userSeed.Role);
            }
        await _db.SaveChangesAsync();
    }
}
