using System;

namespace EFInheritanceConsole.Services.DTO
{
    public class UsuarioTiendaDTO
    {
        public long Id { get; set; }
        public string Tienda { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }
}