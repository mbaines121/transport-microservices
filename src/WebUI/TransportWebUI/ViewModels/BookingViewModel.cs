using BuildingBlocks.Helpers;

namespace TransportWebUI.ViewModels;

public class BookingViewModel
{
    public Guid BookingId { get; set; }
    public ListItem CustomerName { get; set; } = new ListItem();
    public ListItem ShipperName { get; set; } = new ListItem();
    public ListItem SupplierName { get; set; } = new ListItem();
    public string BookingReference { get; set; } = default!;
}
