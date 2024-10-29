using Suppliers.API.Data;

namespace Suppliers.API.SubDomains.Bookings;

public record GetBookingsResult(IEnumerable<Booking> Bookings);

public class BookingsService : IBookingsService
{
    private readonly ApplicationDbContext _context;

    public BookingsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetBookingsResult> GetBookingsAsync()
    {
        var bookings = await _context.Bookings.ToListAsync();

        return new GetBookingsResult(bookings);
    }
}
