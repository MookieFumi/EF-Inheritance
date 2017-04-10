using System;
using System.Collections.Generic;
using System.Linq;

namespace EFInheritanceConsole.Services.DTO
{
    public class CalendarioUsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaAltaEmpresa { get; set; }
        public IEnumerable<CalendarioTiendaDTO> Tiendas { get; set; }
        public IEnumerable<CalendarioVacacionesDTO> Vacaciones { get; set; }
        public IEnumerable<CalendarioAusenciaDTO> Ausencias { get; set; }

        public decimal TotalHoras
        {
            get
            {
                return (from t in Tiendas
                        from tu in t.Turnos
                        select tu).Sum(p => p.Horas);
            }
        }
    }
}