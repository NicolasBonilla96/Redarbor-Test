using Carter;
using Redarbor.Api.Infrastructure;
using Redarbor.Api.Services;
using Redarbor.Application.Core.Abstractions.Services;

namespace Redarbor.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddExceptionHandler<CustomExceptionHandler>()
            .AddScoped<IUser, CurrentUser>()
            .AddProblemDetails()
            .AddHttpContextAccessor()
            .AddSwagger()
            .AddEndpointsApiExplorer()
            .AddCarter()
            .AddSwaggerGen()
            .AddCors(options =>
            {
                var origins = configuration
                    .GetSection("AllowedOrigins").Value!.Split(",");

                options
                    .AddPolicy(
                        name: "CorsPolicy",
                        policy => policy.WithOrigins(origins)
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                    );
            });

        return services;
    }
}
