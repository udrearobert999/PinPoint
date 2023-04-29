namespace Domain.Entities;

public class Tag
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<PinTag> PinTags { get; set; } = null!;
}