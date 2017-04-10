using System.Data.Entity.ModelConfiguration;
using EFInheritanceConsole.Model.Entities;

namespace EFInheritanceConsole.Model.Configurations
{
    public class PresupuestoUsuarioAusenciaConfiguration : EntityTypeConfiguration<PresupuestoUsuarioAusencia>
    {
        public PresupuestoUsuarioAusenciaConfiguration()
        {
            ToTable("PresupuestosUsuariosAusencias");
        }
    }
}