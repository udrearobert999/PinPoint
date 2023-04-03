using Microsoft.AspNetCore.Identity;

namespace PinPoint.Entities;

public class User : IdentityUser<Guid>
{
    public byte[]? ProfilePicture { get; set; }
}