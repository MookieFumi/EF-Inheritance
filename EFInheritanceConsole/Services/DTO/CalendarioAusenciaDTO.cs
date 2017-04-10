using System;

namespace EFInheritanceConsole.Services.DTO
{
    public class CalendarioAusenciaDTO
    {
        public long Id { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string Observaciones { get; set; }
    }
}