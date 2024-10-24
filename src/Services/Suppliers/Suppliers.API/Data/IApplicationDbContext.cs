using Suppliers.API.Models;

namespace Suppliers.API.Data;

public interface IApplicationDbContext
{
    DbSet<Booking> Bookings { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}