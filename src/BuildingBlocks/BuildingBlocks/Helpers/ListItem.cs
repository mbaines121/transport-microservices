namespace BuildingBlocks.Helpers;

public class ListItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;

    public ListItem()
    {
    }

    public ListItem(Guid id, string name, string code)
    {
        Id = id;
        Name = name;
        Code = code;
    }
}
