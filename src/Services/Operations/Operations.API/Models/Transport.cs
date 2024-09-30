namespace Operations.API.Models;

public class Transport
{
    public DateTime StartDate { get; set; }
    public string ModeOfTransport { get; set; }
    public decimal TotalCost { get; set; }
    public List<Leg> Legs { get; set; }
}
