using System;
using System.Collections.Generic;

namespace EFInheritanceConsole.Model.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual DateTime FechaAltaEmpresa { get; set; }
        public virtual ICollection<PresupuestoUsuarioAusencia> Ausencias { get; set; }
        public virtual ICollection<PresupuestoUsuarioVacaciones> Vacaciones { get; set; }
        public virtual ICollection<PresupuestoUsuarioTienda> Tiendas { get; set; }
    }
}