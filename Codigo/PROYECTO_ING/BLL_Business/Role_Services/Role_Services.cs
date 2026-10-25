using BE_Entities.Permissions;
using BE_Entities.Users;
using DAL_DataAccess.DataEntities;
using BLL_Business.Integrity_Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Business.Role_Services
{
    public class Role_Services
    {
        private readonly Role_Data role_data;
        private readonly DigitoVerificador_Services dv_services;

        public Role_Services()
        {
            role_data = new Role_Data();
            dv_services = new DigitoVerificador_Services();
        }

        #region CONSTRUCCIÓN EN MEMORIA

        public Role CrearRol(string nombre, string descripcion, params IPermission[] permisos)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El rol debe tener un nombre.");

            var rol = new Role(0, nombre, descripcion);

            foreach (var permiso in permisos ?? Array.Empty<IPermission>())
            {
                rol.AsignarPermiso(permiso);
            }

            return rol;
        }

        #endregion

        #region PERSISTENCIA - ROLES

        public int Guardar(Role rol) => role_data.Insertar(rol);

        public int Editar(Role rol) => role_data.Editar(rol);

        /// <summary>El SP borra también las filas de role_permissions y user_roles del rol: se ajustan sus DV.</summary>
        public int Borrar(Role rol) => dv_services.Ejecutar(() => role_data.Borrar(rol),
            new AlcanceDV(TablasVerificables.ROL_PERMISOS, "id_role", rol.Id),
            new AlcanceDV(TablasVerificables.USUARIO_ROLES, "id_role", rol.Id));

        public List<Role> ListarTodo() => role_data.ListarTodo();

        public Role ObtenerPorId(int idRol) => role_data.ObtenerPorId(idRol);

        #endregion

        #region ASIGNACIÓN DE PERMISOS A ROLES

        public int AsignarPermiso(int idRol, int idPermiso) => dv_services.Ejecutar(
            () => role_data.AsignarPermiso(idRol, idPermiso), AlcanceRolPermiso(idRol, idPermiso));

        public int QuitarPermiso(int idRol, int idPermiso) => dv_services.Ejecutar(
            () => role_data.QuitarPermiso(idRol, idPermiso), AlcanceRolPermiso(idRol, idPermiso));

        private static AlcanceDV AlcanceRolPermiso(int idRol, int idPermiso)
        {
            return new AlcanceDV(TablasVerificables.ROL_PERMISOS,
                new Dictionary<string, object> { { "id_role", idRol }, { "id_permission", idPermiso } });
        }

        #endregion

        #region ASIGNACIÓN DE ROLES A USUARIOS

        public int AsignarRolAUsuario(int idUsuario, int idRol) => dv_services.Ejecutar(
            () => role_data.AsignarRolAUsuario(idUsuario, idRol), AlcanceUsuarioRol(idUsuario, idRol));

        public int QuitarRolAUsuario(int idUsuario, int idRol) => dv_services.Ejecutar(
            () => role_data.QuitarRolAUsuario(idUsuario, idRol), AlcanceUsuarioRol(idUsuario, idRol));

        private static AlcanceDV AlcanceUsuarioRol(int idUsuario, int idRol)
        {
            return new AlcanceDV(TablasVerificables.USUARIO_ROLES,
                new Dictionary<string, object> { { "id_user", idUsuario }, { "id_role", idRol } });
        }

        public List<Role> ObtenerRolesPorUsuario(int idUsuario) => role_data.ObtenerRolesPorUsuario(idUsuario);

        #endregion

        #region VERIFICACIÓN DE PERMISOS

        /// <summary>
        /// Trae los roles del usuario desde la BD y chequea si alguno le otorga el permiso pedido.
        /// </summary>
        public bool UsuarioTienePermiso(int idUsuario, string nombrePermiso)
        {
            var roles = ObtenerRolesPorUsuario(idUsuario);
            return roles.Any(r => r.TienePermiso(nombrePermiso));
        }

        /// <summary>
        /// Igual que la anterior, pero con roles ya cargados en memoria
        /// (por ejemplo, guardados en el UserAccount tras el login).
        /// </summary>
        public bool UsuarioTienePermiso(IEnumerable<Role> rolesDelUsuario, string nombrePermiso)
        {
            if (rolesDelUsuario == null)
                return false;

            return rolesDelUsuario.Any(rol => rol.TienePermiso(nombrePermiso));
        }

        public List<IPermission> ObtenerPermisosEfectivos(IEnumerable<Role> rolesDelUsuario)
        {
            var resultado = new List<IPermission>();

            foreach (var rol in rolesDelUsuario ?? Enumerable.Empty<Role>())
            {
                resultado.AddRange(rol.ObtenerPermisosEfectivos());
            }

            return resultado
                .GroupBy(p => p.Id)
                .Select(g => g.First())
                .ToList();
        }

        #endregion
    }
}
