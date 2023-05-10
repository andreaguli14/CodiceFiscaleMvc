using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    //override configuring
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=Comuni;Username=postgres;Password=postgres");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comuni>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("comuni3_pkey");

            entity.ToTable("comuni");

            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Comune).HasColumnName("comune");
            entity.Property(e => e.Provincia).HasColumnName("provincia");
            entity.Property(e => e.Regione).HasColumnName("regione");
            entity.Property(e => e.Sigla).HasColumnName("sigla");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
