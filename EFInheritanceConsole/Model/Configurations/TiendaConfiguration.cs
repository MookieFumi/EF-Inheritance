using System.Data.Entity.ModelConfiguration;
using EFInheritanceConsole.Model.Entities;

namespace EFInheritanceConsole.Model.Configurations
{
    public class TiendaConfiguration : EntityTypeConfiguration<Tienda>
    {
        public TiendaConfiguration()
        {
            ToTable("Tiendas");
            Property(p => p.Nombre).IsRequired().HasMaxLength(150);

            //HasMany(p => p.Empleados).WithRequired(p => p.Tienda).WillCascadeOnDelete(true);
        }
    }
}