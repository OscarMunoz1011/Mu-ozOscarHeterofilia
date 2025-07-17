using Dominio;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistencia.Contexto
{
    public class DbContextSql : DbContext
    {
        public DbContextSql(DbContextOptions<DbContextSql> options) : base(options)
        {
            
        }

        #region Tablas
        public DbSet<Cf001pais> Cf001pais { get; set; }
        public DbSet<Ht001deportista> Ht001deportista { get; set; }
        public DbSet<Ht002tipoModalidad> Ht002tipoModalidad { get; set; }
        public DbSet<Ht003modalidad> Ht003modalidad { get; set; }
        public DbSet<Ht004resultado> Ht004resultado { get; set; }
        public DbSet<Sg001usuario> Sg001usuario { get; set; }
        public DbSet<Sg002log> Sg002log { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
