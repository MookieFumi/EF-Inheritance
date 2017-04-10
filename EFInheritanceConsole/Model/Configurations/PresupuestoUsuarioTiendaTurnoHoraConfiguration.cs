using System.Data.Entity.ModelConfiguration;
using EFInheritanceConsole.Model.Entities;

namespace EFInheritanceConsole.Model.Configurations
{
    public class PresupuestoUsuarioTiendaTurnoHoraConfiguration :
        EntityTypeConfiguration<PresupuestoUsuarioTiendaTurnoHora>
    {
        public PresupuestoUsuarioTiendaTurnoHoraConfiguration()
        {
            ToTable("PresupuestosUsuariosTiendasTurnosHoras");
        }
    }
}