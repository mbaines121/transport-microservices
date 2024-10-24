namespace Suppliers.API.Models;

public class Booking
{
    public Guid BookingId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ShipperId { get; set; }
    public Guid SupplierId { get; set; }
}
