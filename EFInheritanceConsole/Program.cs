using EFInheritanceConsole.Model;

namespace EFInheritanceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new AppContext())
            {
                //EliminarEmpresas(context);


                var empresa = new Empresa()
                {
                    Nombre = "Empresa1"
                };

                var turnoEmpresa = new TurnoEmpresa
                {
                    Nombre = "T1 Empresa",
                    Abreviatura = "T1E",
                    Activado = true,
                    Empresa = empresa
                };

                var canal = new Canal
                {
                    Empresa = empresa,
                    Nombre = "Canal1"
                };
                var turnoCanal = new TurnoCanal
                {
                    Nombre = "T1 Canal",
                    Abreviatura = "T1C",
                    Activado = true,
                    Empresa = empresa,
                    Canal = canal
                };

                context.Empresas.Add(empresa);
                context.TurnosEmpresas.Add(turnoEmpresa);
                context.Canales.Add(canal);
                context.TurnosCanales.Add(turnoCanal);
                context.SaveChanges();
            }
        }

        private static void EliminarEmpresas(AppContext context)
        {
            var empresas = context.Empresas;
            foreach (var empresa in empresas)
            {
                context.Empresas.Remove(empresa);
            }
        }
    }
}
