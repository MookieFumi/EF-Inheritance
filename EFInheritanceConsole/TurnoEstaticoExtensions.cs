using System;

namespace EFInheritanceConsole
{
    public static class TurnoEstaticoExtensions
    {
        public static string ToFriendlyString(this TurnoEstatico me)
        {
            switch (me)
            {
                case TurnoEstatico.Mañana:
                    return "Mañana";
                case TurnoEstatico.Tarde:
                    return "Tarde";
                case TurnoEstatico.Noche:
                    return "Noche";
                default:
                    throw new ArgumentOutOfRangeException(nameof(me), me, null);
            }
        }
    }
}