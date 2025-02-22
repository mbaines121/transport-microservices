namespace TransportWebUI.ViewModels;

public class JobViewModel
{
    public string JobNumber { get; set; } = default!;
    public string CustomerName { get; set; } = default!;
    public string ShipperName { get; set; } = default!;
    public string SupplierName { get; set; } = default!;
    public DateTime DateCreated { get; set; }
}
