using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public partial class ComuniContext : DbContext
{
    public ComuniContext()
    {
    }

    public ComuniContext(DbContextOptions<ComuniContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comuni> Comunis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=password;Database=comuni");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comuni>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("comuni");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
