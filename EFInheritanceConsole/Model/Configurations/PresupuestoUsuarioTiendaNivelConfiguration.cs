using System.Data.Entity.ModelConfiguration;
using EFInheritanceConsole.Model.Entities;

namespace EFInheritanceConsole.Model.Configurations
{
    public class PresupuestoUsuarioTiendaNivelConfiguration : EntityTypeConfiguration<PresupuestoUsuarioTiendaNivel>
    {
        public PresupuestoUsuarioTiendaNivelConfiguration()
        {
            ToTable("PresupuestosUsuariosTiendasNiveles");
        }
    }
}