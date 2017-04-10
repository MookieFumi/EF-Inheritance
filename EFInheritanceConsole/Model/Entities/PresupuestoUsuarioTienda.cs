using System;
using System.Collections.Generic;

namespace EFInheritanceConsole.Model.Entities
{
    public class PresupuestoUsuarioTienda : EntidadAuditada
    {
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int TiendaId { get; set; }
        public virtual Tienda Tienda { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }

        public virtual ICollection<PresupuestoUsuarioTiendaNivel> Niveles { get; set; }
        public virtual ICollection<PresupuestoUsuarioTiendaTurnoHora> TurnosHoras { get; set; }

        public void AñadirNivel(PresupuestoUsuarioTiendaNivel nivel)
        {
            nivel.PresupuestoUsuarioTienda = this;
            nivel.PresupuestoUsuarioTiendaId = Id;
            Niveles.Add(nivel);
        }

        public void AñadirTurnoHora(PresupuestoUsuarioTiendaTurnoHora turnoHora)
        {
            turnoHora.PresupuestoUsuarioTienda = this;
            turnoHora.PresupuestoUsuarioTiendaId = Id;
            TurnosHoras.Add(turnoHora);
        }
    }
}