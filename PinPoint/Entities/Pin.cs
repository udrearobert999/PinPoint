﻿namespace PinPoint.Entities;

public class Pin
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public byte[] Picture { get; set; } = null!;

    public Guid UserId { get; set; }

    public ICollection<PinTag> PinTags { get; set; } = null!;
}