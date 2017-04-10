using System;

namespace EFInheritanceConsole.Model.Entities
{
    public class PresupuestoUsuarioTiendaTurnoHora : EntidadAuditada
    {
        public long PresupuestoUsuarioTiendaId { get; set; }
        public virtual PresupuestoUsuarioTienda PresupuestoUsuarioTienda { get; set; }
        public string Abrevitura { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public int Horas { get; set; }
        public int? Minutos { get; set; }
    }
}