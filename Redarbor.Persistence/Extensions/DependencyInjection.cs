using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Domain.Entities.Auth;
using Redarbor.Domain.Interfaces;
using Redarbor.Persistence.Contexts;
using Redarbor.Persistence.Interceptors;
using Redarbor.Persistence.Repositories.Dapper.Connection;
using Redarbor.Persistence.Repositories.Dapper.Queries;
using Redarbor.Persistence.Repositories.EF;

namespace Redarbor.Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services
            .AddDbContext<RedarborDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>())
                    .UseSqlServer(connectionString, _ =>
                    {
                        _.CommandTimeout(60);
                        _.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
            });

        services
            .AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<RedarborDbContext>()
            .AddDefaultTokenProviders();

        services
            .AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<RedarborDbContext>())
            .AddScoped<DbContextInitializer>()
            .AddScoped<ISaveChangesInterceptor, EntityInterceptor>();

        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<ICompanyRepository, CompanyRepository>()
            .AddScoped<IPortalRepository, PortalRepository>()
            .AddScoped<IStateRepository, StateRepository>()
            .AddScoped<IEmployeeRepository, EmployeeReository>()
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
