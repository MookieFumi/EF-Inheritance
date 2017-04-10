using System.Data.Entity.ModelConfiguration;
using EFInheritanceConsole.Model.Entities;

namespace EFInheritanceConsole.Model.Configurations
{
    public class TurnoConfiguration : EntityTypeConfiguration<Turno>
    {
        public TurnoConfiguration()
        {
            ToTable("Turnos");
            Property(p => p.Nombre).IsRequired();
            Property(p => p.Abreviatura).HasMaxLength(3).IsRequired();
        }
    }
}