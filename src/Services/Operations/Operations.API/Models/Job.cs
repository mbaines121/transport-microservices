namespace Operations.API.Models;

public class Job
{
    public Guid JobId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ShipperId { get; set; }
    public Guid SupplierId { get; set; }
    public string JobNumber { get; set; }
    public List<Load> Loads { get; set; }
    public List<Transport> Transports { get; set; }
}
