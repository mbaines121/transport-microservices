using Suppliers.API.Models;

namespace Suppliers.API.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Booking> Bookings =>
        new List<Booking>
        {
            new Booking
            {
                BookingId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                ShipperId = Guid.NewGuid(),
                SupplierId = Guid.NewGuid(),
                BookingReference = "MAB00123"
            }
        };
}