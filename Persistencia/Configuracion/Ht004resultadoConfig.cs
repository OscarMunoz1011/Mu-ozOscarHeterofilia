using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Configuracion
{
    internal class Ht004resultadoConfig : IEntityTypeConfiguration<Ht004resultado>
    {
        public void Configure(EntityTypeBuilder<Ht004resultado> entity)
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
        }
    }
}
