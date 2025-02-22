using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Suppliers.API.Models;

public class Booking
{
    [Key]
    public Guid BookingId { get; set; }

    
    public Guid? CustomerId { get; set; }
    public Guid? ShipperId { get; set; }
    public Guid? SupplierId { get; set; }

    [ForeignKey("CustomerId")]
    public virtual Company Customer { get; set; } = default!;

    [ForeignKey("ShipperId")]
    public virtual Company Shipper { get; set; } = default!;

    [ForeignKey("SupplierId")]
    public virtual Company Supplier { get; set; } = default!;


    public string BookingReference { get; set; } = default!;

    public bool Closed { get; set; }
    public DateTime ClosedDate { get; set; }
}
