using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Configuracion
{
    internal class Sg001usuarioConfig : IEntityTypeConfiguration<Sg001usuario>
    {
        public void Configure(EntityTypeBuilder<Sg001usuario> entity)
        {
            entity.HasKey(e => e.Sg001id).HasName("sg001usuario_pk");

            entity.ToTable("sg001usuario");

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
        }
    }
}
