using System;
using System.Collections.Generic;
using System.Linq;
using EFInheritanceConsole.Model;
using EFInheritanceConsole.Model.Entities;
using EFInheritanceConsole.Services;

namespace EFInheritanceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CalendarioContext())
            {
                //SeedData(context);
                var service = new CalendarioService(context);
                try
                {
                    var usuarioId = context.Usuarios.First().UsuarioId;
                    service.AñadirAusencia(usuarioId, new DateTime(2017, 2, 11), new DateTime(2017, 2, 16));
                    service.AñadirVacaciones(usuarioId, new DateTime(2017, 06, 06), new DateTime(2017, 06, 21));
                    service.AñadirVacaciones(usuarioId, new DateTime(2017, 06, 28), new DateTime(2017, 07, 05));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.ReadKey();
            }
        }

        private static void SeedData(CalendarioContext context)
        {
            EliminarDatosEmpresas(context);
            AñadirDatosTurnos(context);
            context.SaveChanges();
        }

        private static void AñadirDatosTurnos(CalendarioContext context)
        {
            var plenilunio = new Tienda { Nombre = "Plenilunio" };
            var plazaNorte = new Tienda { Nombre = "Plaza Norte" };

            var empresa = new Empresa()
            {
                Nombre = "Empresa 1",
                Turnos = new List<TurnoEmpresa>
                {
                    new TurnoEmpresa
                    {
                        Nombre = "Turno 1 Empresa",
                        Abreviatura = "T1E",
                        Activado = true
                    }
                },
                Canales = new List<Canal>
                {
                    new Canal
                    {
                        Nombre = "Canal 1",
                        Turnos = new List<TurnoCanal>
                        {
                            new TurnoCanal
                            {
                                Nombre = "Turno 1 Canal",
                                Abreviatura = "T1C",
                                Activado = true
                            }
                        },
                        Tiendas = new List<Tienda>
                        {
                            plenilunio,
                            plazaNorte
                        }
                    }
                },
                Usuarios = new List<Usuario>
                {
                    new Usuario
                    {
                        Nombre = "Miguel Angel Martin Hrdez",
                        Ausencias = new List<PresupuestoUsuarioAusencia>
                        {
                            new PresupuestoUsuarioAusencia
                            {
                                FechaDesde = new DateTime(2017, 01, 11),
                                FechaHasta = new DateTime(2017, 01, 12),
                                Observaciones = "Mi ausencia 1"
                            },
                            new PresupuestoUsuarioAusencia
                            {
                                FechaDesde = new DateTime(2017, 01, 28),
                                Observaciones = "Mi ausencia 2"
                            }
                        },
                        Vacaciones = new List<PresupuestoUsuarioVacaciones>
                        {
                            new PresupuestoUsuarioVacaciones
                            {
                                FechaDesde = new DateTime(2017, 01, 14),
                                FechaHasta = new DateTime(2017, 01, 18),
                                Observaciones = "Mis vacaciones 1"
                            }
                        },
                        Tiendas = new List<PresupuestoUsuarioTienda>
                        {
                            new PresupuestoUsuarioTienda
                            {
                                Tienda = plazaNorte,
                                FechaDesde = new DateTime(2017, 1, 1),
                                FechaHasta = new DateTime(2017, 4, 14),
                                TurnosHoras =
                                    GenerarTurnosHoras(new Random(), new DateTime(2017, 1, 1), new DateTime(2017, 4, 14))
                            },
                            new PresupuestoUsuarioTienda
                            {
                                Tienda = plenilunio,
                                FechaDesde = new DateTime(2017, 1, 12),
                                FechaHasta = new DateTime(2017, 12, 31),
                                TurnosHoras =
                                    GenerarTurnosHoras(new Random(), new DateTime(2017, 1, 12),
                                        new DateTime(2017, 12, 31))
                            },
                        }
                    },
                    new Usuario
                    {
                        Nombre = "Montserrat Gomez Rubiano",
                        Tiendas = new List<PresupuestoUsuarioTienda>
                        {
                            new PresupuestoUsuarioTienda
                            {
                                Tienda = plenilunio,
                                FechaDesde = new DateTime(2017, 1, 1),
                                FechaHasta = new DateTime(2017, 12, 31),
                                //TODO: Generar con cabeza
                                TurnosHoras =
                                    GenerarTurnosHoras(new Random(), new DateTime(2017, 1, 1),
                                        new DateTime(2017, 12, 31))
                            }
                        }
                    }
                }
            };
            context.Empresas.Add(empresa);
        }

        private static ICollection<PresupuestoUsuarioTiendaTurnoHora> GenerarTurnosHoras(Random random,
            DateTime fechaDesde, DateTime fechaHasta)
        {
            var items = new List<PresupuestoUsuarioTiendaTurnoHora>();
            var turno = GetTurnoAleatorio(random);
            for (var day = fechaDesde.Date; day <= fechaHasta; day = day.AddDays(1))
            {
                items.Add(new PresupuestoUsuarioTiendaTurnoHora()
                {
                    Fecha = day,
                    Horas = 8,
                    Nombre = turno.ToFriendlyString(),
                    Abrevitura = ((int)turno).ToString()
                });
            }
            return items;
        }

        private static void EliminarDatosEmpresas(CalendarioContext context)
        {
            var empresas = context.Empresas;
            foreach (var empresa in empresas)
            {
                var turnos = context.TurnosEmpresas.Where(p => p.EmpresaId == empresa.EmpresaId);
                foreach (var turno in turnos)
                {
                    context.TurnosEmpresas.Remove(turno);
                }
                context.Empresas.Remove(empresa);
            }
        }

        private static TurnoEstatico GetTurnoAleatorio(Random random)
        {
            var values = Enum.GetValues(typeof(TurnoEstatico));
            return (TurnoEstatico)values.GetValue(random.Next(values.Length));
        }
    }
}