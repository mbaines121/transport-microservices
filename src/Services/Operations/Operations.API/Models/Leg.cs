namespace Operations.API.Models;

public class Leg
{
    public Guid VesselId { get; set; }

    public DateTime DepartureDate { get; set; }
    public DateTime ArrivalDate { get; set; }

    public Guid PolId { get; set; }
    public Guid PodId { get; set; }
}
