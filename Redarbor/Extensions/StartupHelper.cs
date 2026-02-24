using Carter;
using Redarbor.Api.Configurations.Definitions;
using Redarbor.Application.Extensions;
using Redarbor.Infrastructure.Extensions;
using Redarbor.Persistence.Extensions;

namespace Redarbor.Api.Extensions;

internal static class StartupHelper
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder
            .ConfigureAppSettingsEnvironment();

        builder
            .Services
            .RegisterWebServices(builder.Configuration)
            .RegisterApplicationServices(builder.Configuration)
            .RegiterInfrastructureServices(builder.Configuration)
            .RegisterPersistenceServices(builder.Configuration)
            .ConfigureCultureInfo();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeLine(this WebApplication app)
    {
        app.UseExceptionHandler()
            .UseHttpsRedirection()
            .UseStaticFiles()
            .UseCors("CorsPolicy")
            .UseSwaggerUI()
            .UseSwagger();

        app.MapCarter();

        return app;
    }
}
