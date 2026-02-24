using System.Globalization;

namespace Redarbor.Api.Configurations.Definitions;

public static class CultireInfoDefinition
{
    public static IServiceCollection ConfigureCultureInfo(
            this IServiceCollection services,
            string name = "es-CO",
            string currencySymbol = "$",
            string numberDecimalSeparator = ",",
            string numberGroupSeparator = "."
        )
    {
        var _cultiInfo = new CultureInfo(name);
        _cultiInfo.NumberFormat.CurrencySymbol = currencySymbol;
        _cultiInfo.NumberFormat.NumberDecimalSeparator = numberDecimalSeparator;
        _cultiInfo.NumberFormat.NumberGroupSeparator = numberGroupSeparator;

        CultureInfo.DefaultThreadCurrentCulture = _cultiInfo;
        CultureInfo.DefaultThreadCurrentUICulture = _cultiInfo;

        return services;
    }
}
