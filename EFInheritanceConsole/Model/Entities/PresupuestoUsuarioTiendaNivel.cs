using System;

namespace EFInheritanceConsole.Model.Entities
{
    public class PresupuestoUsuarioTiendaNivel : EntidadAuditada
    {
        public long PresupuestoUsuarioTiendaId { get; set; }
        public virtual PresupuestoUsuarioTienda PresupuestoUsuarioTienda { get; set; }
        public TipoUsuarioTienda TipoUsuarioTienda { get; set; }
        public string Nombre { get; set; }
        public int Nivel { get; set; }
        public DateTime FechaDesde { get; set; }
        public bool Principal { get; set; }
    }
}