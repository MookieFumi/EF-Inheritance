using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using EFInheritanceConsole.Model.Configurations;
using EFInheritanceConsole.Model.Entities;

namespace EFInheritanceConsole.Model
{
    public class CalendarioContext : DbContext
    {
        public CalendarioContext()
        {
        }

        public CalendarioContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Canal> Canales { get; set; }
        public DbSet<Tienda> Tiendas { get; set; }
        public DbSet<Usuario> Usuarios { set; get; }

        public DbSet<TurnoCanal> TurnosCanales { get; set; }
        public DbSet<TurnoEmpresa> TurnosEmpresas { get; set; }
        public DbSet<PresupuestoUsuarioAusencia> PresupuestosUsuariosAusencias { get; set; }
        public DbSet<PresupuestoUsuarioVacaciones> PresupuestosUsuariosVacaciones { get; set; }
        public DbSet<PresupuestoUsuarioTienda> PresupuestosUsuariosTiendas { get; set; }
        public DbSet<PresupuestoUsuarioTiendaNivel> PresupuestosUsuariosTiendasNiveles { get; set; }
        public DbSet<PresupuestoUsuarioTiendaTurnoHora> PresupuestosUsuariosTiendasTurnosHoras { get; set; }

        public override int SaveChanges()
        {
            var entidadesNuevas = ChangeTracker.Entries<EntidadAuditada>()
                .Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);

            var entidadesModificadas = ChangeTracker.Entries<EntidadAuditada>()
                .Where(p => p.State == EntityState.Modified)
                .Select(p => p.Entity);

            //TODO: System.Threading.Thread.CurrentPrincipal.Identity.Name
            var usuario = "miguel.martin@analyticalways.com";
            if (string.IsNullOrWhiteSpace(usuario))
            {
                usuario = null;
            }
            var fechaActual = DateTime.UtcNow;

            foreach (var added in entidadesNuevas)
            {
                added.CreadoPor = usuario;
                added.FechaCreacion = fechaActual;
            }

            foreach (var modified in entidadesModificadas)
            {
                modified.ModificadoPor = usuario;
                modified.FechaModificacion = fechaActual;
            }

            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var error in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"", error.PropertyName, error.ErrorMessage);
                    }
                }
                throw;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(CanalConfiguration)));
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}