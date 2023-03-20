using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PinPoint.Entities;

public partial class PinPointContext : IdentityDbContext
{
    public PinPointContext()
    {
    }

    public PinPointContext(DbContextOptions<PinPointContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TestTable> TestTables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<TestTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TestTable");

            entity.Property(e => e.Test)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
