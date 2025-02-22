namespace TransportWebUI.Models;

public class Job
{
    public Guid JobId { get; set; }
    public Guid BookingId { get; set; }
    public string JobNumber { get; set; } = default!;
    public Company? Customer { get; set; }
    public Company? Supplier { get; set; }
    public Company? Shipper { get; set; }
    public DateTime DateCreated { get; set; }
}
