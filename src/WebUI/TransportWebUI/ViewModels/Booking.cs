namespace TransportWebUI.ViewModels;

public class Booking
{
    public Guid BookingId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ShipperId { get; set; }
    public Guid SupplierId { get; set; }
    public string BookingReference { get; set; } = default!;
}
