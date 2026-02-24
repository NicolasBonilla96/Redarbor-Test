using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redarbor.Application.Common.Behaviors;

namespace Redarbor.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddValidatorsFromAssembly(AssemblyReference.Assembly)
            .AddMediatR(configure =>
                {
                    configure.RegisterServicesFromAssembly(AssemblyReference.Assembly);
                    configure.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                });

        return services;
    }
}
