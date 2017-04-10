using System.Data.Entity.ModelConfiguration;
using EFInheritanceConsole.Model.Entities;

namespace EFInheritanceConsole.Model.Configurations
{
    public class PresupuestoUsuarioTiendaConfiguration : EntityTypeConfiguration<PresupuestoUsuarioTienda>
    {
        public PresupuestoUsuarioTiendaConfiguration()
        {
            ToTable("PresupuestosUsuariosTiendas");

            HasMany(p => p.TurnosHoras)
                .WithRequired(p => p.PresupuestoUsuarioTienda)
                //.HasForeignKey(p => p.PresupuestoUsuarioTiendaId)
                .WillCascadeOnDelete(true);

            HasMany(p => p.Niveles)
                .WithRequired(p => p.PresupuestoUsuarioTienda)
                //.HasForeignKey(p => p.PresupuestoUsuarioTiendaId)
                .WillCascadeOnDelete(true);
        }
    }
}