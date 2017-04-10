using System.Collections.Generic;

namespace EFInheritanceConsole.Services.DTO
{
    public class CalendarioTiendaDTO : TiendaDTO
    {
        public IEnumerable<CalendarioTurnoHoraDTO> Turnos { get; set; }
    }
}