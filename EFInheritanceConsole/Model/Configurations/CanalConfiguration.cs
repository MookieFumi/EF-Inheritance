using System.Data.Entity.ModelConfiguration;

namespace EFInheritanceConsole.Model.Configurations
{
    public class CanalConfiguration : EntityTypeConfiguration<Canal>
    {
        public CanalConfiguration()
        {
            ToTable("Canales");
            Property(p => p.Nombre).IsRequired().HasMaxLength(150);
        }
    }
}