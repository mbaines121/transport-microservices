namespace Suppliers.API.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedBookingAsync(context);
    }

    private static async Task SeedBookingAsync(ApplicationDbContext context)
    {
        if (!await context.Bookings.AnyAsync())
        {
            await context.Bookings.AddRangeAsync(InitialData.Bookings);
            await context.SaveChangesAsync();
        }
    }
}
