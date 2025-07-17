using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Configuracion
{
    internal class Ht002tipoModalidadConfig : IEntityTypeConfiguration<Ht002tipoModalidad>
    {
        public void Configure(EntityTypeBuilder<Ht002tipoModalidad> entity)
        {
            entity.HasKey(e => e.Ht002id).HasName("ht002tipo_modalidad_pk");

            entity.ToTable("ht002tipo_modalidad");

            entity.Property(e => e.Ht002id).HasColumnName("ht002id");
            entity.Property(e => e.Ht002esArranque).HasColumnName("ht002es_arranque");
            entity.Property(e => e.Ht002esEnvion).HasColumnName("ht002es_envion");
        }
    }
}
