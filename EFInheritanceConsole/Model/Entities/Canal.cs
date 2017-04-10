using System.Collections.Generic;

namespace EFInheritanceConsole.Model.Entities
{
    public class Canal
    {
        public int CanalId { get; set; }
        public string Nombre { get; set; }

        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public virtual ICollection<Tienda> Tiendas { get; set; }
        public virtual ICollection<TurnoCanal> Turnos { get; set; }
    }
}