using System.Data.Entity.ModelConfiguration;
using EFInheritanceConsole.Model.Entities;

namespace EFInheritanceConsole.Model.Configurations
{
    public class TurnoCanalConfiguration : EntityTypeConfiguration<TurnoCanal>
    {
        public TurnoCanalConfiguration()
        {
            HasRequired(p => p.Canal).WithMany().WillCascadeOnDelete(true);
        }
    }
}