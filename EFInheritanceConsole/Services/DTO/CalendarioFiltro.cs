using System.Collections.Generic;

namespace EFInheritanceConsole.Services.DTO
{
    public class CalendarioFiltro
    {
        public AñoMes AñoMes { get; set; }
        public IEnumerable<int> Canales { get; set; }
        public IEnumerable<int> Responsables { get; set; }
        public IEnumerable<int> Tiendas { get; set; }
        public IEnumerable<int> Vendedores { get; set; }
    }
}