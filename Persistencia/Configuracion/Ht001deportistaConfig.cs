using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Configuracion
{
    internal class Ht001deportistaConfig : IEntityTypeConfiguration<Ht001deportista>
    {
        public void Configure(EntityTypeBuilder<Ht001deportista> entity)
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
        }
    }
}
