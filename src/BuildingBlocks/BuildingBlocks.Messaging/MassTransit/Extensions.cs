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
                // TODO: First request a token credential from https://servicebus.azure.net.

                //TokenCredential credential = new ClientSecretCredential("<tenant_id>", "<client_id>", "<client_secret>");
                //var client = new ServiceBusClient("<fully_qualified_namespace>", credential);

                config.UsingAzureServiceBus((context, configurator) =>
                {
                    // Then pass the token credential in.
                    configurator.Host(configuration["MessageBroker:Host"], host =>
                    {
                        host.TokenCredential = new DefaultAzureCredential(
                            new DefaultAzureCredentialOptions
                            {
                                ManagedIdentityClientId = configuration["MessageBroker:ClientId"]
                            });
                    });

                    configurator.ConfigureEndpoints(context);
                });
            }
        });

        return services;
    }
}
