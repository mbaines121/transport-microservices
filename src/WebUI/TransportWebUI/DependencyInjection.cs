namespace TransportWebUI;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IJobService, JobService>();

        return services;
    }
}
