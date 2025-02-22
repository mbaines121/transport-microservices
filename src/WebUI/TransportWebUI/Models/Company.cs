using BuildingBlocks.Helpers;

namespace TransportWebUI.Models;

public class Company
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Address { get; set; } = default!;

    public ListItem ToListItem() => new ListItem(CompanyId, Name, Code);
}
