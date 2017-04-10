using System.Collections.Generic;

namespace EFInheritanceConsole.Model.Entities
{
    public class Tienda
    {
        public int TiendaId { get; set; }
        public string Nombre { get; set; }
        public int CanalId { get; set; }
        public virtual Canal Canal { get; set; }
        public virtual ICollection<PresupuestoUsuarioTienda> Empleados { get; set; }
    }
}