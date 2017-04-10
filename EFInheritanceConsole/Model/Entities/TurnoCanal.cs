namespace EFInheritanceConsole.Model.Entities
{
    public class TurnoCanal : Turno
    {
        public int CanalId { get; set; }
        public virtual Canal Canal { get; set; }
    }
}