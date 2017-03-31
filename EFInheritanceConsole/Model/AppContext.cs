using System.Data.Entity;
using System.Reflection;
using EFInheritanceConsole.Model.Configurations;

namespace EFInheritanceConsole.Model
{
    public class AppContext : DbContext
    {
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Canal> Canales { get; set; }
        public DbSet<TurnoCanal> TurnosCanales { get; set; }
        public DbSet<TurnoEmpresa> TurnosEmpresas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(CanalConfiguration)));
        }
    }
}