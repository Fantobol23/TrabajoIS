using BE_Entities.Permissions;
using BE_Entities.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccess.DataEntities
{
    public class Role_Data : Mapper<Role>
    {
        #region CONSTRUCTOR

        public Role_Data()
        {
            acceso = new AccessDB();
            permission_data = new Permission_Data();
        }

        private readonly Permission_Data permission_data;

        #endregion

        #region CREATE

        public override int Insertar(Role rol)
        {
            int res;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@role_name", rol.Name));
                parametros.Add(acceso.CrearParametro("@description", rol.Description));

                SqlParameter idSalida = new SqlParameter("@id_role", SqlDbType.Int);
                idSalida.Direction = ParameterDirection.Output;
                parametros.Add(idSalida);

                acceso.AbrirConexionBD();
                res = acceso.EscribirBD("SP_Roles_Insertar", parametros);

                rol.Id = (int)idSalida.Value;

                return res;
            }
            catch
            {
                return -1;
            }
            finally
            {
                acceso.CerrarConexionBD();
                GC.Collect();
            }
        }

        #endregion

        #region READ

        /// <summary>
        /// Lista plana de roles (id, nombre, descripción), SIN sus permisos cargados.
        /// Para eso usar ObtenerPorId, que sí arma el árbol completo de cada permiso.
        /// </summary>
        public override List<Role> ListarTodo()
        {
            try
            {
                List<Role> roles = new List<Role>();

                acceso.AbrirConexionBD();
                DataTable tb = acceso.LeerBD("SP_Roles_ObtenerTodos");

                foreach (DataRow row in tb.Rows)
                {
                    roles.Add(MapearFila(row));
                }

                return roles;
            }
            catch
            {
                throw new Exception("Error al leer la base de datos");
            }
            finally
            {
                acceso.CerrarConexionBD();
                GC.Collect();
            }
        }

        /// <summary>
        /// Trae un rol puntual con sus permisos ya resueltos (simples y compuestos,
        /// estos últimos con sus hijos completos).
        /// </summary>
        public Role ObtenerPorId(int idRole)
        {
            Role rol = null;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_role", idRole)
                };

                acceso.AbrirConexionBD();
                DataTable tb = acceso.LeerBD("SP_Roles_ObtenerPorId", parametros);
                acceso.CerrarConexionBD();

                if (tb.Rows.Count == 0)
                    return null;

                rol = MapearFila(tb.Rows[0]);
                CargarPermisos(rol);

                return rol;
            }
            catch
            {
                throw new Exception("Error al leer la base de datos");
            }
        }

        /// <summary>
        /// Todos los roles asignados a un usuario, cada uno con sus permisos ya resueltos.
        /// Pensado para usarse en el login: cargar esto una vez y guardarlo en el UserAccount.
        /// </summary>
        public List<Role> ObtenerRolesPorUsuario(int idUser)
        {
            List<Role> roles = new List<Role>();

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_user", idUser)
                };

                acceso.AbrirConexionBD();
                DataTable tb = acceso.LeerBD("SP_UserRoles_ObtenerPorUsuario", parametros);
                acceso.CerrarConexionBD();

                foreach (DataRow row in tb.Rows)
                {
                    Role rol = MapearFila(row);
                    CargarPermisos(rol);
                    roles.Add(rol);
                }

                return roles;
            }
            catch
            {
                throw new Exception("Error al leer la base de datos");
            }
        }

        private void CargarPermisos(Role rol)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_role", rol.Id)
            };

            acceso.AbrirConexionBD();
            DataTable tbPermisos = acceso.LeerBD("SP_RolePermissions_ObtenerPorRol", parametros);
            acceso.CerrarConexionBD();

            // El árbol completo se resuelve una sola vez y de ahí se toman
            // por referencia los permisos (simples o compuestos) del rol.
            List<IPermission> arbolCompleto = permission_data.ObtenerArbolCompleto();

            foreach (DataRow row in tbPermisos.Rows)
            {
                int idPermiso = int.Parse(row["id_permission"].ToString());
                IPermission permiso = arbolCompleto.Find(p => p.Id == idPermiso);

                if (permiso != null)
                    rol.AsignarPermiso(permiso);
            }
        }

        private Role MapearFila(DataRow row)
        {
            int id = int.Parse(row["id_role"].ToString());
            string nombre = row["role_name"].ToString();
            string descripcion = row["description"] == DBNull.Value ? null : row["description"].ToString();

            return new Role(id, nombre, descripcion);
        }

        #endregion

        #region UPDATE

        public override int Editar(Role rol)
        {
            int respuesta;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_role", rol.Id));
                parametros.Add(acceso.CrearParametro("@role_name", rol.Name));
                parametros.Add(acceso.CrearParametro("@description", rol.Description));

                acceso.AbrirConexionBD();
                respuesta = acceso.EscribirBD("SP_Roles_Editar", parametros);

                return respuesta;
            }
            catch
            {
                return -1;
            }
            finally
            {
                acceso.CerrarConexionBD();
                GC.Collect();
            }
        }

        #endregion

        #region DELETE

        public override int Borrar(Role rol)
        {
            int respuesta;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_role", rol.Id));

                acceso.AbrirConexionBD();
                respuesta = acceso.EscribirBD("SP_Roles_Borrar", parametros);

                return respuesta;
            }
            catch
            {
                return -1;
            }
            finally
            {
                acceso.CerrarConexionBD();
                GC.Collect();
            }
        }

        #endregion

        #region ASIGNACIÓN DE PERMISOS A ROLES

        public int AsignarPermiso(int idRole, int idPermission)
        {
            int respuesta;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_role", idRole));
                parametros.Add(acceso.CrearParametro("@id_permission", idPermission));

                acceso.AbrirConexionBD();
                respuesta = acceso.EscribirBD("SP_RolePermissions_Asignar", parametros);

                return respuesta;
            }
            catch
            {
                return -1;
            }
            finally
            {
                acceso.CerrarConexionBD();
                GC.Collect();
            }
        }

        public int QuitarPermiso(int idRole, int idPermission)
        {
            int respuesta;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_role", idRole));
                parametros.Add(acceso.CrearParametro("@id_permission", idPermission));

                acceso.AbrirConexionBD();
                respuesta = acceso.EscribirBD("SP_RolePermissions_Quitar", parametros);

                return respuesta;
            }
            catch
            {
                return -1;
            }
            finally
            {
                acceso.CerrarConexionBD();
                GC.Collect();
            }
        }

        #endregion

        #region ASIGNACIÓN DE ROLES A USUARIOS

        public int AsignarRolAUsuario(int idUser, int idRole)
        {
            int respuesta;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_user", idUser));
                parametros.Add(acceso.CrearParametro("@id_role", idRole));

                acceso.AbrirConexionBD();
                respuesta = acceso.EscribirBD("SP_UserRoles_Asignar", parametros);

                return respuesta;
            }
            catch
            {
                return -1;
            }
            finally
            {
                acceso.CerrarConexionBD();
                GC.Collect();
            }
        }

        public int QuitarRolAUsuario(int idUser, int idRole)
        {
            int respuesta;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_user", idUser));
                parametros.Add(acceso.CrearParametro("@id_role", idRole));

                acceso.AbrirConexionBD();
                respuesta = acceso.EscribirBD("SP_UserRoles_Quitar", parametros);

                return respuesta;
            }
            catch
            {
                return -1;
            }
            finally
            {
                acceso.CerrarConexionBD();
                GC.Collect();
            }
        }

        #endregion
    }
}
