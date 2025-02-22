namespace Suppliers.API.SubDomains.Bookings;

public interface IBookingsService
{
    Task<GetBookingsResult> GetBookingsAsync();

    Task SaveBookingAsync(BookingViewModel bookingViewModel, CancellationToken cancellationToken);
    Task ToJobAsync(BookingViewModel bookingViewModel, CancellationToken cancellationToken);
}
