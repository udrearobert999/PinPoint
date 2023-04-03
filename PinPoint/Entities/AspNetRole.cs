namespace PinPoint.Entities;

public class AspNetRole
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; } = new List<AspNetRoleClaim>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
