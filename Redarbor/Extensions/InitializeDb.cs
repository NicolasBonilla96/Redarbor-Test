using Redarbor.Persistence.Extensions;

namespace Redarbor.Api.Extensions;

public static class InitializeDb
{
    public static async Task InitializeDbAsync(this WebApplication app)
    {
        using var _scope = app.Services.CreateScope();
        var _intializer = _scope.ServiceProvider.GetRequiredService<DbContextInitializer>();
        await _intializer.InitializeAsync();
        if(app.Environment.IsDevelopment())
            await _intializer.SeedAsync();
    }
}
