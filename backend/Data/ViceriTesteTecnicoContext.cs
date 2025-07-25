using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data;

public partial class ViceriTesteTecnicoContext : DbContext
{
    public ViceriTesteTecnicoContext()
    {
    }

    public ViceriTesteTecnicoContext(DbContextOptions<ViceriTesteTecnicoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Heroi> Herois { get; set; }

    public virtual DbSet<HeroisSuperpoderes> HeroisSuperpoderes { get; set; }

    public virtual DbSet<Superpoderes> Superpoderes { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Heroi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Herois__3214EC07DE6C0A8A");

            entity.Property(e => e.Nome).HasMaxLength(120);
            entity.Property(e => e.NomeHeroi).HasMaxLength(120);
        });

        modelBuilder.Entity<HeroisSuperpoderes>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.Heroi).WithMany()
                .HasForeignKey(d => d.HeroiId)
                .HasConstraintName("FK_HeroiId");

            entity.HasOne(d => d.Superpoder).WithMany()
                .HasForeignKey(d => d.SuperpoderId)
                .HasConstraintName("FK_SuperpoderId");
        });

        modelBuilder.Entity<Superpoderes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Superpod__3214EC079B3232D1");

            entity.Property(e => e.Descricao).HasMaxLength(250);
            entity.Property(e => e.Superpoder).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
