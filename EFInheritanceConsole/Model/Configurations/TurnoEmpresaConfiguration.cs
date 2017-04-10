using System.Data.Entity.ModelConfiguration;
using EFInheritanceConsole.Model.Entities;

namespace EFInheritanceConsole.Model.Configurations
{
    public class TurnoEmpresaConfiguration : EntityTypeConfiguration<TurnoEmpresa>
    {
        public TurnoEmpresaConfiguration()
        {
            //HasRequired(p => p.Empresa).WithMany().WillCascadeOnDelete(true);
        }
    }
}