using System.Data.Entity.ModelConfiguration;

namespace EFInheritanceConsole.Model.Configurations
{
    public class EmpresaConfiguration : EntityTypeConfiguration<Empresa>
    {
        public EmpresaConfiguration()
        {
            ToTable("Empresas");
            Property(p => p.Nombre).IsRequired().HasMaxLength(150);
        }
    }
}