using System.Data.Entity.ModelConfiguration;
using EFInheritanceConsole.Model.Entities;

namespace EFInheritanceConsole.Model.Configurations
{
    public class CanalConfiguration : EntityTypeConfiguration<Canal>
    {
        public CanalConfiguration()
        {
            ToTable("Canales");
            Property(p => p.Nombre).IsRequired().HasMaxLength(150);

            HasMany(p => p.Tiendas).WithRequired(p => p.Canal).WillCascadeOnDelete(true);
            HasMany(p => p.Turnos).WithRequired(p => p.Canal).WillCascadeOnDelete(true);
        }
    }
}