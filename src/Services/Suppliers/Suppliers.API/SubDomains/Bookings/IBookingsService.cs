
namespace Suppliers.API.SubDomains.Bookings;

public interface IBookingsService
{
    Task<GetBookingsResult> GetBookingsAsync();
}
