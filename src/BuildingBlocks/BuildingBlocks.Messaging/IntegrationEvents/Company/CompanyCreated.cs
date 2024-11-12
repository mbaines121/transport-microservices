namespace BuildingBlocks.Messaging.IntegrationEvents.Company;

public class CompanyCreated : IntegrationEvent
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Address { get; set; } = default!;
}
