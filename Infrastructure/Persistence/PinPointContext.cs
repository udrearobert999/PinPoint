﻿using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class PinPointContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public PinPointContext(DbContextOptions<PinPointContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pin> Pins { get; set; } = null!;
    public virtual DbSet<Tag> Tags { get; set; } = null!;
    public virtual DbSet<PinTag> PinTags { get; set; } = null!;
    public virtual DbSet<PinComment> PinComment { get; set; } = null!;
    public virtual DbSet<Like> Likes { get; set; } = null!;

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

            entity.Property(e => e.Title).IsRequired();

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

        modelBuilder.Entity<PinComment>(entity =>
        {
            entity.HasKey(e => e.Id);

            modelBuilder.Entity<Pin>()
                .Property(pin => pin.CreatedDate)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            entity.HasOne<Pin>(e => e.Pin)
                .WithMany(e => e.PinComments)
                .HasForeignKey(e => e.PinId);

            entity.HasOne<User>(e => e.User)
                .WithMany(e => e.PinComments)
                .HasForeignKey(e => e.UserId);
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne<Pin>(e => e.Pin)
                .WithMany(e => e.Likes)
                .HasForeignKey(e => e.PinId);

            entity.HasOne<User>(e => e.User)
                .WithMany(e => e.Likes)
                .HasForeignKey(e => e.UserId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}