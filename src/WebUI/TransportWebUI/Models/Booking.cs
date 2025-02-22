namespace TransportWebUI.Models;

public class Booking
{
    public Guid BookingId { get; set; }
    public Company? Customer { get; set; }
    public Company? Supplier { get; set; }
    public Company? Shipper { get; set; }

    public string BookingReference { get; set; } = default!;
}
