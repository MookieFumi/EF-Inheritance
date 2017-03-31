namespace EFInheritanceConsole.Model
{
    //This pattern of making a database table for each entity class is called table per type (TPT) inheritance.
    public abstract class Turno
    {
        public int TurnoId { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public bool Activado { get; set; }

        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}