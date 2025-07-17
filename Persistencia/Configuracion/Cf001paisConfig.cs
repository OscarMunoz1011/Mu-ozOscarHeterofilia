using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Configuracion
{
    public class Cf001paisConfig : IEntityTypeConfiguration<Cf001pais>
    {
        public void Configure(EntityTypeBuilder<Cf001pais> entity)
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
        }
    }
}
