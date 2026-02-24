using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redarbor.Application.Core.Abstractions.Services;
using Redarbor.Infrastructure.Services;
using Redarbor.Infrastructure.Services.Auth;
using Redarbor.Infrastructure.Services.Auth.Settings;


namespace Redarbor.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegiterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddCustomAuthentication(configuration);
        
        services
            .AddScoped<IUserRoleService, UserRoleService>()
            .AddScoped<IAuthService, AuthService>();

        services
            .AddOptions<JwtSettings>()
            .BindConfiguration(nameof(JwtSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
