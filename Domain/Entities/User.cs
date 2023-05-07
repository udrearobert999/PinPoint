using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>, IEntity<Guid>
{
    public byte[]? ProfilePicture { get; set; }

    public ICollection<PinComment> PinComments { get; set; } = null!;
    public ICollection<Like> Likes { get; set; } = null!;
}