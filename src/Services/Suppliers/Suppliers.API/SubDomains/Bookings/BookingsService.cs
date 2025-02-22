using BuildingBlocks.Exceptions;
using BuildingBlocks.Messaging.IntegrationEvents.Booking;

namespace Suppliers.API.SubDomains.Bookings;

public record GetBookingsResult(IEnumerable<Booking> Bookings);

public class BookingsService(IApplicationDbContext _context, IPublishEndpoint _publishEndpoint) : IBookingsService
{
    public async Task<GetBookingsResult> GetBookingsAsync()
    {
        var bookings = await _context.Bookings
            .Include(m => m.Customer)
            .Include(m => m.Supplier)
            .Include(m => m.Shipper)
            .ToListAsync();

        return new GetBookingsResult(bookings);
    }

    public async Task SaveBookingAsync(BookingViewModel bookingViewModel, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings.FindAsync(bookingViewModel.BookingId);

        if (booking is not null)
        {
            booking = bookingViewModel.Adapt<Booking>();

            _context.Bookings.Update(booking);
        }
        else
        {
            booking = new Booking();

            booking.CustomerId = bookingViewModel.CustomerName.Id;
            booking.SupplierId = bookingViewModel.SupplierName.Id;
            booking.ShipperId = bookingViewModel.ShipperName.Id;
            booking.BookingReference = bookingViewModel.BookingReference;

            _context.Bookings.Add(booking);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task ToJobAsync(BookingViewModel bookingViewModel, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings.FindAsync(bookingViewModel.BookingId);

        if (booking is null)
        {
            throw new InternalServerException("Unable to convert the booking to a job. The booking could not be found.");
        }

        booking.Closed = true;
        booking.ClosedDate = DateTime.UtcNow;

        var result = await _context.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            throw new InternalServerException("Unable to convert the booking to a job. The booking could not be closed.");
        }

        await _publishEndpoint.Publish(new BookingToJobEvent
        {
            BookingId = bookingViewModel.BookingId,
            CustomerId = bookingViewModel.CustomerName.Id,
            SupplierId = bookingViewModel.SupplierName.Id,
            ShipperId = bookingViewModel.ShipperName.Id,
            BookingReference = bookingViewModel.BookingReference
        });
    }
}
