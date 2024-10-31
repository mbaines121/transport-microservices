using Suppliers.API.Data;
using Suppliers.API.SubDomains.Bookings;

namespace Suppliers.API;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("Database");

        // TODO: Get the token credential?

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            //options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBookingsService, BookingsService>();

        return services;
    }
}
