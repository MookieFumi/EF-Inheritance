using System;

namespace EFInheritanceConsole.Model.Entities
{
    public class PresupuestoUsuarioAusencia : EntidadAuditada
    {
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string Observaciones { get; set; }
    }
}