using System;
using System.Collections.Generic;
using System.Linq;
using EFInheritanceConsole.Model;
using EFInheritanceConsole.Model.Entities;
using EFInheritanceConsole.Services.DTO;

namespace EFInheritanceConsole.Services
{
    public class CalendarioService : ICalendarioService
    {
        private readonly CalendarioContext _context;

        public CalendarioService(CalendarioContext context)
        {
            _context = context;
        }

        #region Pantalla principal

        public PaginatedResult<CalendarioUsuarioDTO> GetCalendario(PaginatedConfiguration pagination, CalendarioFiltro filtro)
        {
            var ultimoDiaMes = DateTime.DaysInMonth(filtro.AñoMes.Año, filtro.AñoMes.Mes);
            var fechaInicioMes = new DateTime(filtro.AñoMes.Año, filtro.AñoMes.Mes, 1);
            var fechaFinMes = new DateTime(filtro.AñoMes.Año, filtro.AñoMes.Mes, ultimoDiaMes);

            var query = from u in _context.Usuarios
                        select new CalendarioUsuarioDTO
                        {
                            Id = u.UsuarioId,
                            Nombre = u.Nombre,
                            FechaAltaEmpresa = u.FechaAltaEmpresa,
                            Tiendas = from t in u.Tiendas
                                      select new CalendarioTiendaDTO()
                                      {
                                          Id = t.TiendaId,
                                          Nombre = t.Tienda.Nombre,
                                          Turnos = from tu in t.TurnosHoras
                                                   where tu.Fecha >= fechaInicioMes && tu.Fecha <= fechaFinMes
                                                   select new CalendarioTurnoHoraDTO
                                                   {
                                                       Turno = tu.Abrevitura,
                                                       Dia = tu.Fecha.Day,
                                                       Horas = tu.Horas,
                                                       Minutos = tu.Minutos
                                                   }
                                      },
                            Ausencias = from a in u.Ausencias
                                        where a.FechaDesde >= fechaInicioMes && a.FechaDesde <= fechaFinMes ||
                                              a.FechaHasta >= fechaInicioMes && a.FechaHasta <= fechaFinMes ||
                                              a.FechaDesde >= fechaInicioMes && a.FechaDesde <= fechaFinMes || a.FechaHasta == null
                                        select new CalendarioAusenciaDTO
                                        {
                                            Id = a.Id,
                                            FechaDesde = a.FechaDesde,
                                            FechaHasta = a.FechaHasta,
                                            Observaciones = a.Observaciones
                                        },
                            Vacaciones = from a in u.Vacaciones
                                         where a.FechaDesde >= fechaInicioMes && a.FechaDesde <= fechaFinMes ||
                                               a.FechaHasta >= fechaInicioMes && a.FechaHasta <= fechaFinMes
                                         select new CalendarioVacacionesDTO
                                         {
                                             Id = a.Id,
                                             FechaDesde = a.FechaDesde,
                                             FechaHasta = a.FechaHasta,
                                             Observaciones = a.Observaciones
                                         },
                        };

            IEnumerable<CalendarioUsuarioDTO> data;
            int count;
            if (pagination.Enabled)
            {
                count = query.Count();
                if (count == 0)
                {
                    data = Enumerable.Empty<CalendarioUsuarioDTO>();
                }
                else
                {
                    var currentPage = pagination.PageIndex;
                    var skip = (currentPage < 1 ? 0 : currentPage - 1) * pagination.PageSize;
                    data =
                        query.Skip(skip)
                            .Take(pagination.PageSize)
                            .ToList();
                }
            }
            else
            {
                data = query.ToList();
                count = data.Count();
            }
            return new PaginatedResult<CalendarioUsuarioDTO>(pagination.PageIndex, pagination.PageSize, data, count);
        }

        public IEnumerable<UsuarioDTO> GetUsuarios(IEnumerable<int> tiendas, IEnumerable<int> responsables, IEnumerable<int> canales)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TiendaDTO> GetTiendas(IEnumerable<int> responsables, IEnumerable<int> canales)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UsuarioDTO> GetResponsables(IEnumerable<int> canales)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CanalDTO> GetCanales(int empresaId)
        {
            throw new NotImplementedException();
        }

        public void CopiarUsuario(int usuarioId, int año, int mes, IEnumerable<int> usuarios)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Ausencias

        public PaginatedResult<CalendarioAusenciaDTO> GetAusencias(PaginatedConfiguration pagination, int usuarioId)
        {
            var query = from a in
                _context.PresupuestosUsuariosAusencias
                        where a.UsuarioId == usuarioId
                        orderby a.FechaDesde descending
                        select new CalendarioAusenciaDTO
                        {
                            Id = a.Id,
                            FechaDesde = a.FechaDesde,
                            FechaHasta = a.FechaHasta,
                            Observaciones = a.Observaciones
                        };


            IEnumerable<CalendarioAusenciaDTO> data;
            int count;
            if (pagination.Enabled)
            {
                count = query.Count();
                if (count == 0)
                {
                    data = Enumerable.Empty<CalendarioAusenciaDTO>();
                }
                else
                {
                    var currentPage = pagination.PageIndex;
                    var skip = (currentPage < 1 ? 0 : currentPage - 1) * pagination.PageSize;
                    data =
                        query.Skip(skip)
                            .Take(pagination.PageSize)
                            .ToList();
                }
            }
            else
            {
                data = query.ToList();
                count = data.Count();
            }

            return new PaginatedResult<CalendarioAusenciaDTO>(pagination.PageIndex, pagination.PageSize, data, count);
        }

        public long AñadirAusencia(int usuarioId, DateTime fechaDesde, DateTime? fechaHasta, string observaciones = null)
        {
            if (fechaHasta?.Date < fechaDesde.Date)
            {
                throw new DecisionPlatformException("Fecha hasta debe ser menor que Fecha desde");
            }
            if (ExisteAusencia(usuarioId, fechaDesde, fechaHasta))
            {
                throw new DecisionPlatformException("Ya existe una ausencia en el mismo intervalo de tiempo");
            }
            if (_context.Usuarios.Find(usuarioId).FechaAltaEmpresa > fechaDesde)
            {
                throw new DecisionPlatformException("No se pueden introducir datos anteriores a la fecha de alta en la empresa");
            }

            var ausencia = new PresupuestoUsuarioAusencia
            {
                UsuarioId = usuarioId,
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                Observaciones = observaciones
            };
            _context.PresupuestosUsuariosAusencias.Add(ausencia);
            _context.SaveChanges();
            return ausencia.Id;
        }

        private bool ExisteAusencia(int usuarioId, DateTime fechaDesde, DateTime? fechaHasta)
        {
            var query = from a in _context.PresupuestosUsuariosAusencias
                        where a.UsuarioId == usuarioId && a.FechaDesde >= fechaDesde
                        select a;
            query = !fechaHasta.HasValue
                ? query.Where(p => p.FechaHasta == null)
                : query.Where(p => p.FechaHasta <= fechaHasta || fechaHasta <= p.FechaHasta);
            return query.Any();
        }

        public void EliminarAusencia(long id)
        {
            var ausencia = _context.PresupuestosUsuariosAusencias.Find(id);
            _context.PresupuestosUsuariosAusencias.Remove(ausencia);
            _context.SaveChanges();
        }

        #endregion

        #region Vacaciones

        public PaginatedResult<CalendarioVacacionesDTO> GetVacaciones(PaginatedConfiguration pagination, int usuarioId)
        {
            var query = from v in
                _context.PresupuestosUsuariosVacaciones
                        orderby v.FechaDesde descending
                        where v.UsuarioId == usuarioId
                        select new CalendarioVacacionesDTO
                        {
                            Id = v.Id,
                            FechaDesde = v.FechaDesde,
                            FechaHasta = v.FechaHasta,
                            Observaciones = v.Observaciones
                        };

            int count;

            IEnumerable<CalendarioVacacionesDTO> data;
            if (pagination.Enabled)
            {
                count = query.Count();
                if (count == 0)
                {
                    data = Enumerable.Empty<CalendarioVacacionesDTO>();
                }
                else
                {
                    var currentPage = pagination.PageIndex;
                    var skip = (currentPage < 1 ? 0 : currentPage - 1) * pagination.PageSize;
                    data =
                        query.Skip(skip)
                            .Take(pagination.PageSize)
                            .ToList();
                }
            }
            else
            {
                data = query.ToList();
                count = data.Count();
            }
            return new PaginatedResult<CalendarioVacacionesDTO>(pagination.PageIndex, pagination.PageSize, data, count);
        }

        public long AñadirVacaciones(int usuarioId, DateTime fechaDesde, DateTime fechaHasta,
            string observaciones = null)
        {
            if (fechaHasta.Date < fechaDesde.Date)
            {
                throw new DecisionPlatformException("Fecha hasta debe ser menor que Fecha desde");
            }
            if (ExisteVacaciones(usuarioId, fechaDesde, fechaHasta))
            {
                throw new DecisionPlatformException("Ya existen unas vacaciones en el mismo intervalo de tiempo");
            }
            if (_context.Usuarios.Find(usuarioId).FechaAltaEmpresa > fechaDesde)
            {
                throw new DecisionPlatformException("No se pueden introducir datos anteriores a la fecha de alta en la empresa");
            }

            var vacaciones = new PresupuestoUsuarioVacaciones()
            {
                UsuarioId = usuarioId,
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                Observaciones = observaciones
            };
            _context.PresupuestosUsuariosVacaciones.Add(vacaciones);
            _context.SaveChanges();
            return vacaciones.Id;
        }

        private bool ExisteVacaciones(int usuarioId, DateTime fechaDesde, DateTime fechaHasta)
        {
            var query = from a in _context.PresupuestosUsuariosAusencias
                        where a.UsuarioId == usuarioId &&
                              a.FechaDesde >= fechaDesde && a.FechaDesde <= fechaHasta ||
                              a.FechaHasta >= fechaDesde && a.FechaHasta <= fechaHasta
                        select a;
            return query.Any();
        }

        public void EliminarVacaciones(long id)
        {
            var vacaciones = _context.PresupuestosUsuariosVacaciones.Find(id);
            _context.PresupuestosUsuariosVacaciones.Remove(vacaciones);
            _context.SaveChanges();
        }

        #endregion

        #region UsuariosTiendas

        public PaginatedResult<UsuarioTiendaDTO> GetUsuariosTiendas(PaginatedConfiguration pagination, int usuarioId)
        {
            var query = from ut in _context.PresupuestosUsuariosTiendas
                        orderby ut.FechaDesde descending
                        where ut.UsuarioId == usuarioId
                        select new UsuarioTiendaDTO
                        {
                            Id = ut.Id,
                            Tienda = ut.Tienda.Nombre,
                            FechaDesde = ut.FechaDesde,
                            FechaHasta = ut.FechaHasta,
                        };

            int count;

            IEnumerable<UsuarioTiendaDTO> data;
            if (pagination.Enabled)
            {
                count = query.Count();
                if (count == 0)
                {
                    data = Enumerable.Empty<UsuarioTiendaDTO>();
                }
                else
                {
                    var currentPage = pagination.PageIndex;
                    var skip = (currentPage < 1 ? 0 : currentPage - 1) * pagination.PageSize;
                    data =
                        query.Skip(skip)
                            .Take(pagination.PageSize)
                            .ToList();
                }
            }
            else
            {
                data = query.ToList();
                count = data.Count();
            }
            return new PaginatedResult<UsuarioTiendaDTO>(pagination.PageIndex, pagination.PageSize, data, count);
        }

        public long AñadirUsuarioTienda(int tiendaId, int usuarioId, DateTime fechaDesde, DateTime? fechaHasta)
        {
            if (fechaHasta?.Date < fechaDesde.Date)
            {
                throw new DecisionPlatformException("Fecha hasta debe ser menor que Fecha desde");
            }
            if (ExisteUsuarioTienda(tiendaId, usuarioId, fechaDesde, fechaHasta))
            {
                throw new DecisionPlatformException("Ya existe el usuario en la tienda en el mismo intervalo de tiempo");
            }
            if (_context.Usuarios.Find(usuarioId).FechaAltaEmpresa > fechaDesde)
            {
                throw new DecisionPlatformException("No se pueden introducir datos anteriores a la fecha de alta en la empresa");
            }

            var usuarioTienda = new PresupuestoUsuarioTienda()
            {
                TiendaId = tiendaId,
                UsuarioId = usuarioId,
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta
            };
            _context.PresupuestosUsuariosTiendas.Add(usuarioTienda);
            _context.SaveChanges();
            return usuarioTienda.Id;
        }

        public void EliminarUsuarioTienda(long id)
        {
            var usuarioTienda = _context.PresupuestosUsuariosTiendas.Find(id);
            _context.PresupuestosUsuariosTiendas.Remove(usuarioTienda);
            _context.SaveChanges();
        }

        private bool ExisteUsuarioTienda(int tiendaId, int usuarioId, DateTime fechaDesde, DateTime? fechaHasta)
        {
            var query = from a in _context.PresupuestosUsuariosTiendas
                        where a.TiendaId == tiendaId && a.UsuarioId == usuarioId && a.FechaDesde >= fechaDesde
                        select a;
            query = !fechaHasta.HasValue
                ? query.Where(p => p.FechaHasta == null)
                : query.Where(p => p.FechaHasta <= fechaHasta || fechaHasta <= p.FechaHasta);
            return query.Any();
        }

        #endregion

        #region UsuariosTiendasNiveles

        public PaginatedResult<UsuarioTiendaNivelDTO> GetUsuariosTiendasNiveles(PaginatedConfiguration pagination,
            int usuarioTiendaId)
        {
            var query = from utn in
                _context.PresupuestosUsuariosTiendasNiveles
                        orderby utn.FechaDesde descending
                        where utn.PresupuestoUsuarioTiendaId == usuarioTiendaId
                        select new UsuarioTiendaNivelDTO
                        {
                            Id = utn.Id,
                            UsuarioTienda = new UsuarioTiendaDTO()
                            {
                                Id = utn.PresupuestoUsuarioTienda.Id,
                                FechaDesde = utn.PresupuestoUsuarioTienda.FechaDesde,
                                FechaHasta = utn.PresupuestoUsuarioTienda.FechaHasta,
                                Tienda = utn.PresupuestoUsuarioTienda.Tienda.Nombre,
                            },
                            TipoUsuarioTienda = utn.TipoUsuarioTienda,
                            Nombre = utn.Nombre,
                            Nivel = utn.Nivel,
                            FechaDesde = utn.FechaDesde,
                            Principal = utn.Principal
                        };

            int count;

            IEnumerable<UsuarioTiendaNivelDTO> data;
            if (pagination.Enabled)
            {
                count = query.Count();
                if (count == 0)
                {
                    data = Enumerable.Empty<UsuarioTiendaNivelDTO>();
                }
                else
                {
                    var currentPage = pagination.PageIndex;
                    var skip = (currentPage < 1 ? 0 : currentPage - 1) * pagination.PageSize;
                    data =
                        query.Skip(skip)
                            .Take(pagination.PageSize)
                            .ToList();
                }
            }
            else
            {
                data = query.ToList();
                count = data.Count();
            }
            return new PaginatedResult<UsuarioTiendaNivelDTO>(pagination.PageIndex, pagination.PageSize, data, count);
        }

        public long AñadirUsuarioTiendaNivel(int usuarioTiendaId, DateTime fechaDesde,
            TipoUsuarioTienda tipoUsuarioTienda, string nombre, int nivel)
        {
            var usuarioTienda = _context.PresupuestosUsuariosTiendas.Single(p => p.Id == usuarioTiendaId);
            usuarioTienda.AñadirNivel(new PresupuestoUsuarioTiendaNivel
            {
                FechaDesde = fechaDesde,
                TipoUsuarioTienda = tipoUsuarioTienda,
                Nombre = nombre,
                Nivel = nivel
            });
            _context.SaveChanges();
            return usuarioTienda.Id;
        }

        public void EliminarUsuarioTiendaNivel(long id)
        {
            var tiendaNivel = _context.PresupuestosUsuariosTiendasNiveles.Find(id);
            _context.PresupuestosUsuariosTiendasNiveles.Remove(tiendaNivel);
            _context.SaveChanges();
        }

        #endregion

        #region UsuariosTiendasTurnosHoras

        public long AñadirUsuarioTiendaTurnoHora(int usuarioTiendaId, DateTime fecha, string abreviatura, string nombre,
            int horas, int? minutos)
        {
            var usuarioTienda = _context.PresupuestosUsuariosTiendas.Single(p => p.Id == usuarioTiendaId);
            usuarioTienda.AñadirTurnoHora(new PresupuestoUsuarioTiendaTurnoHora
            {
                Abrevitura = abreviatura,
                Nombre = nombre,
                Fecha = fecha,
                Horas = horas,
                Minutos = minutos
            });
            _context.SaveChanges();
            return usuarioTienda.Id;
        }

        private PresupuestoUsuarioTiendaTurnoHora GetUsuarioTiendaTurnoHora(long id)
        {
            return _context.PresupuestosUsuariosTiendasTurnosHoras.Find(id);
        }

        public void ActualizarUsuarioTiendaTurnoHora(int id, int horas, int? minutos)
        {
            var turnoHora = GetUsuarioTiendaTurnoHora(id);
            turnoHora.Horas = horas;
            turnoHora.Minutos = minutos;
            _context.SaveChanges();
        }

        public void EliminarUsuarioTiendaTurnoHora(long id)
        {
            var turnoHora = GetUsuarioTiendaTurnoHora(id);
            _context.PresupuestosUsuariosTiendasTurnosHoras.Remove(turnoHora);
            _context.SaveChanges();
        }

        #endregion
    }
}