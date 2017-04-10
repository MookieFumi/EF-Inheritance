namespace EFInheritanceConsole.Model.Entities
{
    //This pattern of making a database table for each entity class is called table per type (TPT) inheritance.
    public abstract class Turno : EntidadAuditada
    {
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public bool Activado { get; set; }
    }
}