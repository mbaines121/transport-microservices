using Suppliers.API.Models;

namespace Suppliers.API.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Booking> Bookings =>
        new List<Booking>
        {
            new Booking{ BookingId = Guid.NewGuid() }
        };
}