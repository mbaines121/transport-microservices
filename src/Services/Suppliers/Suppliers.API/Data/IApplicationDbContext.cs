namespace Suppliers.API.Data;

public interface IApplicationDbContext
{
    DbSet<Booking> Bookings { get; }

    DbSet<Company> Companies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}