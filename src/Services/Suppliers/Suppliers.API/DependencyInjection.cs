using Microsoft.Data.SqlClient;
using Suppliers.API.Data;
using Suppliers.API.SubDomains.Bookings;

namespace Suppliers.API;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            if (!environment.IsDevelopment())
            {
                SqlAuthenticationProvider.SetProvider(SqlAuthenticationMethod.ActiveDirectoryManagedIdentity, new CustomSqlAuthProvider());
            }

            var sqlConnection = new SqlConnection(connectionString);

            options.UseSqlServer(sqlConnection);
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
