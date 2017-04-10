using System;

namespace EFInheritanceConsole.Model.Entities
{
    public abstract class EntidadAuditada : PrespuestoEntityBase
    {
        public string CreadoPor { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

    public class PrespuestoEntityBase
    {
        public long Id { get; set; }
    }
}