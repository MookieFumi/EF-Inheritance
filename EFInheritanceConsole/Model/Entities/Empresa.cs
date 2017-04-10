using System.Collections.Generic;

namespace EFInheritanceConsole.Model.Entities
{
    public class Empresa
    {
        public int EmpresaId { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Canal> Canales { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<TurnoEmpresa> Turnos { get; set; }
    }
}