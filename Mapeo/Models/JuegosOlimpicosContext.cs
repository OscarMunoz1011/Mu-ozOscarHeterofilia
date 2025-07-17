using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Mapeo.Models;

public partial class JuegosOlimpicosContext : DbContext
{
    public JuegosOlimpicosContext()
    {
    }

    public JuegosOlimpicosContext(DbContextOptions<JuegosOlimpicosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cf001pai> Cf001pais { get; set; }

    public virtual DbSet<Ht001deportistum> Ht001deportista { get; set; }

    public virtual DbSet<Ht002tipoModalidad> Ht002tipoModalidads { get; set; }

    public virtual DbSet<Ht003modalidad> Ht003modalidads { get; set; }

    public virtual DbSet<Ht004resultado> Ht004resultados { get; set; }

    public virtual DbSet<Sg001usuario> Sg001usuarios { get; set; }

    public virtual DbSet<Sg002log> Sg002logs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:juegosolimpicososcarmunoz.database.windows.net,1433;Initial Catalog=juegosolimpicos;Persist Security Info=False;User ID=oscarmunoz;Password=Oscar1011;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cf001pai>(entity =>
        {
            entity.HasKey(e => e.Cf001codigo).HasName("cf001pais_pk");

            entity.ToTable("cf001pais");

            entity.Property(e => e.Cf001codigo)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("cf001codigo");
            entity.Property(e => e.Cf001descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cf001descripcion");
            entity.Property(e => e.Cf001estado)
                .HasDefaultValue(true)
                .HasColumnName("cf001estado");
            entity.Property(e => e.Cf001nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cf001nombre");
        });

        modelBuilder.Entity<Ht001deportistum>(entity =>
        {
            entity.HasKey(e => e.Ht001id).HasName("ht001deportista_pk");

            entity.ToTable("ht001deportista");

            entity.Property(e => e.Ht001id).HasColumnName("ht001id");
            entity.Property(e => e.Cf001codigo)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("cf001codigo");
            entity.Property(e => e.Ht001apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ht001apellido");
            entity.Property(e => e.Ht001estado)
                .HasDefaultValue(true)
                .HasColumnName("ht001estado");
            entity.Property(e => e.Ht001nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ht001nombre");

            entity.HasOne(d => d.Cf001codigoNavigation).WithMany(p => p.Ht001deportista)
                .HasForeignKey(d => d.Cf001codigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ht001deportista_cf001pais_FK");
        });

        modelBuilder.Entity<Ht002tipoModalidad>(entity =>
        {
            entity.HasKey(e => e.Ht002id).HasName("ht002tipo_modalidad_pk");

            entity.ToTable("ht002tipo_modalidad");

            entity.Property(e => e.Ht002id).HasColumnName("ht002id");
            entity.Property(e => e.Ht002esArranque).HasColumnName("ht002es_arranque");
            entity.Property(e => e.Ht002esEnvion).HasColumnName("ht002es_envion");
        });

        modelBuilder.Entity<Ht003modalidad>(entity =>
        {
            entity.HasKey(e => e.Ht003codigo).HasName("ht003modalidad_pk");

            entity.ToTable("ht003modalidad");

            entity.Property(e => e.Ht003codigo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ht003codigo");
            entity.Property(e => e.Ht002id).HasColumnName("ht002id");
            entity.Property(e => e.Ht003descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ht003descripcion");
            entity.Property(e => e.Ht003estado)
                .HasDefaultValue(true)
                .HasColumnName("ht003estado");
            entity.Property(e => e.Ht003nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ht003nombre");

            entity.HasOne(d => d.Ht002).WithMany(p => p.Ht003modalidads)
                .HasForeignKey(d => d.Ht002id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ht003modalidad_ht002tipo_modalidad_FK");
        });

        modelBuilder.Entity<Ht004resultado>(entity =>
        {
            entity.HasKey(e => e.Ht004id).HasName("ht004resultado_pk");

            entity.ToTable("ht004resultado");

            entity.Property(e => e.Ht004id).HasColumnName("ht004id");
            entity.Property(e => e.Ht001id).HasColumnName("ht001id");
            entity.Property(e => e.Ht003codigo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ht003codigo");
            entity.Property(e => e.Ht004intento).HasColumnName("ht004intento");
            entity.Property(e => e.Ht004puntaje).HasColumnName("ht004puntaje");

            entity.HasOne(d => d.Ht001).WithMany(p => p.Ht004resultados)
                .HasForeignKey(d => d.Ht001id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ht004resultado_ht001deportista_FK");

            entity.HasOne(d => d.Ht003codigoNavigation).WithMany(p => p.Ht004resultados)
                .HasForeignKey(d => d.Ht003codigo)
                .HasConstraintName("ht004resultado_ht003modalidad_FK");
        });

        modelBuilder.Entity<Sg001usuario>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sg001usuario");

            entity.Property(e => e.Sg001apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sg001apellido");
            entity.Property(e => e.Sg001estado)
                .HasDefaultValue(true)
                .HasColumnName("sg001estado");
            entity.Property(e => e.Sg001id)
                .ValueGeneratedOnAdd()
                .HasColumnName("sg001id");
            entity.Property(e => e.Sg001nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sg001nombre");
            entity.Property(e => e.Sg001password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sg001password");
            entity.Property(e => e.Sg001usuario1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sg001usuario");
        });

        modelBuilder.Entity<Sg002log>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sg002logs");

            entity.Property(e => e.Sg002codigo)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("sg002codigo");
            entity.Property(e => e.Sg002datos)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("sg002datos");
            entity.Property(e => e.Sg002fecha)
                .HasColumnType("datetime")
                .HasColumnName("sg002fecha");
            entity.Property(e => e.Sg002id)
                .ValueGeneratedOnAdd()
                .HasColumnName("sg002id");
            entity.Property(e => e.Sg002mensaje)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sg002mensaje");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
