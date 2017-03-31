using System.Data.Entity.ModelConfiguration;

namespace EFInheritanceConsole.Model.Configurations
{
    public class TurnoConfiguration : EntityTypeConfiguration<Turno>
    {
        public TurnoConfiguration()
        {
            ToTable("Turnos");
            Property(p => p.Nombre).IsRequired();
            Property(p => p.Abreviatura).HasMaxLength(3).IsRequired();


            HasRequired(p => p.Empresa)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}