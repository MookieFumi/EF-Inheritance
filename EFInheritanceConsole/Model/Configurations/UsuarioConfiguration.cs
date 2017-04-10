using System.Data.Entity.ModelConfiguration;
using EFInheritanceConsole.Model.Entities;

namespace EFInheritanceConsole.Model.Configurations
{
    public class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration()
        {
            ToTable("Usuarios");
            Property(p => p.Nombre).IsRequired().HasMaxLength(150);

            HasMany(p => p.Ausencias).WithRequired(p => p.Usuario).WillCascadeOnDelete(true);
            HasMany(p => p.Vacaciones).WithRequired(p => p.Usuario).WillCascadeOnDelete(true);
            HasMany(p => p.Tiendas).WithRequired(p => p.Usuario).WillCascadeOnDelete(true);
        }
    }
}