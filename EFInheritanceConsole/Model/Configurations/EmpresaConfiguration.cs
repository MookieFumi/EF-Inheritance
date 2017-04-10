using System.Data.Entity.ModelConfiguration;
using EFInheritanceConsole.Model.Entities;

namespace EFInheritanceConsole.Model.Configurations
{
    public class EmpresaConfiguration : EntityTypeConfiguration<Empresa>
    {
        public EmpresaConfiguration()
        {
            ToTable("Empresas");
            Property(p => p.Nombre).IsRequired().HasMaxLength(150);

            HasMany(p => p.Canales).WithRequired(p => p.Empresa).WillCascadeOnDelete(true);
            HasMany(p => p.Usuarios).WithRequired(p => p.Empresa).WillCascadeOnDelete(true);
        }
    }
}