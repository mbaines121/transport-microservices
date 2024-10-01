using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Operations.API.Configurations;

namespace Operations.API.Extensions;

public static class ProgramExtensions
{
    public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        var appName = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name ?? throw new ApplicationException("Could not read assembly name.");

        services.Configure<ConnectionStringConfiguration>(configurationManager.GetSection($"{appName}:connection-string"));
        services.Configure<SettingsConfiguration>(configurationManager.GetSection($"{appName}:settings"));

        // Get the connection string to the App Configuration service. The same connection string is used by every environment. This must be set locally, and in Azure.
        var connectionString = Environment.GetEnvironmentVariable("APPCONFIG_CONNECTIONSTRING") ?? throw new ApplicationException("Could not read APPCONFIG_CONNECTIONSTRING environment variable.");

        // Get the current environment variable. This must be set locally and in Azure.
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? throw new ApplicationException("Could not read ASPNETCORE_ENVIRONMENT environment variable.");

        // Load configuration from Azure App Configuration.
        configurationManager.AddAzureAppConfiguration(options =>
        {
            options.Connect(connectionString)
                .ConfigureKeyVault(kv => kv.SetCredential(new DefaultAzureCredential()))
                .Select(KeyFilter.Any, environment);
            options.ConfigureRefresh(refresh => {
                refresh
                    .Register($"{appName}:settings:myconfiguration")
                    .SetCacheExpiration(TimeSpan.FromMinutes(1));
            });
            options.UseFeatureFlags();
        });

        return services;
    }
}
