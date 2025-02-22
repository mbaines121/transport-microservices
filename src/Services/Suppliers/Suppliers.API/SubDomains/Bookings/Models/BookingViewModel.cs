using BuildingBlocks.Helpers;

namespace Suppliers.API.SubDomains.Bookings.Models;

public class BookingViewModel
{
    public Guid BookingId { get; set; }
    public ListItem CustomerName { get; set; } = default!;
    public ListItem ShipperName { get; set; } = default!;
    public ListItem SupplierName { get; set; } = default!;
    public string BookingReference { get; set; } = default!;
}
