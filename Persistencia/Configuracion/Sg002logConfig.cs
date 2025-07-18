using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Configuracion
{
    internal class Sg002logConfig : IEntityTypeConfiguration<Sg002log>
    {
        public void Configure(EntityTypeBuilder<Sg002log> entity)
        {
            entity.HasKey(e => e.Sg002id).HasName("sg002logs_pk");

            entity.ToTable("sg002logs");

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
        }
    }
}
