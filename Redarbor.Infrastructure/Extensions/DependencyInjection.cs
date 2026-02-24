using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Core.Abstractions.Services;
using Redarbor.Infrastructure.Persistence.Connection;
using Redarbor.Infrastructure.Persistence.Queries;
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

        services
            .AddScoped<ISqlConnectionFactory, SqlConnectionFactory>()
            .AddScoped<IEmployeeQueryRepository, EmployeeQueryRepository>()
            .AddScoped<IUserQueryRepository, UserQueryRepository>()
            .AddScoped<IRoleQueryRepository, RoleQueryRepository>()
            .AddScoped<ICompanyQueryRepository, CompanyQueryRepository>()
            .AddScoped<IPortalQueryRepository, PortalQueryRepository>()
            .AddScoped<IStateQueryRepository, StateQueryRepository>();

        return services;
    }
}
