namespace Domain.Entities;

public class Pin : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public byte[] Picture { get; set; } = null!;

    public Guid UserId { get; set; }

    public ICollection<PinTag> PinTags { get; set; } = null!;
    public ICollection<PinComment> PinComments { get; set; } = null!;
    public ICollection<Like> Likes { get; set; } = null!;
}