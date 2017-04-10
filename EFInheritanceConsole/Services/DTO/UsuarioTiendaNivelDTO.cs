using System;
using EFInheritanceConsole.Model.Entities;

namespace EFInheritanceConsole.Services.DTO
{
    public class UsuarioTiendaNivelDTO
    {
        public long Id { get; set; }
        public UsuarioTiendaDTO UsuarioTienda { get; set; }
        public TipoUsuarioTienda TipoUsuarioTienda { get; set; }
        public string Nombre { get; set; }
        public int Nivel { get; set; }
        public DateTime FechaDesde { get; set; }
        public bool Principal { get; set; }
    }
}