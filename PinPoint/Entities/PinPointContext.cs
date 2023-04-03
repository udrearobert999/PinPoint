using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PinPoint.Entities;

public partial class PinPointContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public PinPointContext(DbContextOptions<PinPointContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pin> Pins { get; set; } = null!;
    public virtual DbSet<Tag> Tags { get; set; } = null!;
    public virtual DbSet<PinTag> PinTags { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity => { entity.Property(e => e.ProfilePicture); });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name);
        });

        modelBuilder.Entity<Pin>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Description);

            entity.Property(e => e.Picture)
                .IsRequired();
        });

        modelBuilder.Entity<PinTag>(entity =>
        {
            entity.HasOne<Pin>(e => e.Pin)
                .WithMany(e => e.PinTags)
                .HasForeignKey(e => e.PinId);

            entity.HasOne<Tag>(e => e.Tag)
                .WithMany(e => e.PinTags)
                .HasForeignKey(e => e.TagId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}