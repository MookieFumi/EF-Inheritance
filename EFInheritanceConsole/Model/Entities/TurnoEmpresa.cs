namespace EFInheritanceConsole.Model.Entities
{
    public class TurnoEmpresa : Turno
    {
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}