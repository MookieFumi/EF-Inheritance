using System.Data.Entity.ModelConfiguration;
using EFInheritanceConsole.Model.Entities;

namespace EFInheritanceConsole.Model.Configurations
{
    public class PresupuestoUsuarioVacacionesConfiguration : EntityTypeConfiguration<PresupuestoUsuarioVacaciones>
    {
        public PresupuestoUsuarioVacacionesConfiguration()
        {
            ToTable("PresupuestosUsuariosVacaciones");
        }
    }
}