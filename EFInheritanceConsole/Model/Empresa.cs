using System.Collections.Generic;

namespace EFInheritanceConsole.Model
{
    public class Empresa
    {
        public int EmpresaId { get; set; }
        public string Nombre { get; set; }

        public virtual IEnumerable<Canal> Canales { get; set; }
        public virtual IEnumerable<TurnoEmpresa> Turnos { get; set; }
    }
}