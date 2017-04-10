using System;
using System.Collections.Generic;
using EFInheritanceConsole.Model.Entities;
using EFInheritanceConsole.Services.DTO;

namespace EFInheritanceConsole.Services
{
    public interface ICalendarioService
    {
        #region Pantalla principal

        PaginatedResult<CalendarioUsuarioDTO> GetCalendario(PaginatedConfiguration pagination, CalendarioFiltro filtro);

        IEnumerable<UsuarioDTO> GetUsuarios(IEnumerable<int> tiendas, IEnumerable<int> responsables, IEnumerable<int> canales);
        IEnumerable<TiendaDTO> GetTiendas(IEnumerable<int> responsables, IEnumerable<int> canales);
        IEnumerable<UsuarioDTO> GetResponsables(IEnumerable<int> canales);
        IEnumerable<CanalDTO> GetCanales(int empresaId);
        void CopiarUsuario(int usuarioId, int año, int mes, IEnumerable<int> usuarios);

        #endregion

        #region Ausencias

        PaginatedResult<CalendarioAusenciaDTO> GetAusencias(PaginatedConfiguration pagination, int usuarioId);
        long AñadirAusencia(int usuarioId, DateTime fechaDesde, DateTime? fechaHasta, string observaciones);
        void EliminarAusencia(long id);

        #endregion

        #region Vacaciones

        PaginatedResult<CalendarioVacacionesDTO> GetVacaciones(PaginatedConfiguration pagination, int usuarioId);
        long AñadirVacaciones(int usuarioId, DateTime fechaDesde, DateTime fechaHasta, string observaciones);
        void EliminarVacaciones(long id);

        #endregion

        #region UsuariosTiendas

        PaginatedResult<UsuarioTiendaDTO> GetUsuariosTiendas(PaginatedConfiguration pagination, int usuarioId);
        long AñadirUsuarioTienda(int tiendaId, int usuarioId, DateTime fechaDesde, DateTime? fechaHasta);
        void EliminarUsuarioTienda(long id);

        #endregion

        #region UsuariosTiendasNiveles

        PaginatedResult<UsuarioTiendaNivelDTO> GetUsuariosTiendasNiveles(PaginatedConfiguration pagination, int usuarioTiendaId);

        long AñadirUsuarioTiendaNivel(int usuarioTiendaId, DateTime fechaDesde, TipoUsuarioTienda tipoUsuarioTienda, string nombre, int nivel);

        void EliminarUsuarioTiendaNivel(long id);

        #endregion

        #region UsuariosTiendasTurnosHoras

        long AñadirUsuarioTiendaTurnoHora(int usuarioTiendaId, DateTime fecha, string abreviatura, string nombre, int horas, int? minutos);

        void ActualizarUsuarioTiendaTurnoHora(int id, int horas, int? minutos);
        void EliminarUsuarioTiendaTurnoHora(long id);

        #endregion
    }
}