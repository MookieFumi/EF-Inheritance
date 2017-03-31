using System.Collections.Generic;

namespace EFInheritanceConsole.Model
{
    public class Canal
    {
        public int CanalId { get; set; }
        public string Nombre { get; set; }

        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public virtual IEnumerable<TurnoCanal> Turnos { get; set; }
    }
}