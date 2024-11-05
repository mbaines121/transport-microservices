using Azure.Identity;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Messaging.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isDevelopment,
        Assembly? assembly = null)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            if (assembly is not null)  
            {
                config.AddConsumers(assembly);
            }

            if (isDevelopment)
            {
                config.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                    {
                        host.Username(configuration["MessageBroker:UserName"]!);
                        host.Password(configuration["MessageBroker:Password"]!);
                    });

                    configurator.ConfigureEndpoints(context);
                });
            }
            else
            {
                config.UsingAzureServiceBus((context, configurator) =>
                {
                    configurator.Host(configuration["MessageBroker:Host"], host =>
                    {
                        host.TokenCredential = new ManagedIdentityCredential(configuration["MessageBroker:ClientId"]);
                    });

                    configurator.ConfigureEndpoints(context);
                });
            }
        });

        return services;
    }
}
