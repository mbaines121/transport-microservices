namespace Operations.API.Models;

public class Transport
{
    public DateTime StartDate { get; set; }
    public string ModeOfTransport { get; set; } = default!;
    public decimal TotalCost { get; set; }
    public List<Leg> Legs { get; set; } = new List<Leg>();
}
