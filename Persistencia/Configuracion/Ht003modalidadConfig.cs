using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Configuracion
{
    internal class Ht003modalidadConfig : IEntityTypeConfiguration<Ht003modalidad>
    {
        public void Configure(EntityTypeBuilder<Ht003modalidad> entity)
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
        }
    }
}
