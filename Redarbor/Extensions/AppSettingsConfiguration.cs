using dotenv.net;

namespace Redarbor.Api.Extensions;

public static class AppSettingsConfiguration
{
    public static void ConfigureAppSettingsEnvironment(this WebApplicationBuilder builder)
    {
        DotEnv.Load();

        var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        builder
            .Host
            .ConfigureAppConfiguration((hostingcontext, config) =>
            {
                config
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: true);
            });
    }
}