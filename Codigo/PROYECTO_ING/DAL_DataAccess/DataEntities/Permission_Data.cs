using BE_Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccess.DataEntities
{
    public class Permission_Data : Mapper<IPermission>
    {
        #region CONSTRUCTOR

        public Permission_Data()
        {
            acceso = new AccessDB();
        }

        #endregion

        #region CREATE

        /// <summary>
        /// Inserta un permiso (simple o compuesto, se detecta por el tipo de instancia).
        /// No inserta la composición (hijos); para eso usar AsignarHijo.
        /// </summary>
        public override int Insertar(IPermission permiso)
        {
            int res;

            try
            {
                string tipo = permiso is CompositePermission ? "COMPOSITE" : "SIMPLE";

                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@permission_name", permiso.Name));
                parametros.Add(acceso.CrearParametro("@description", permiso.Description));
                parametros.Add(acceso.CrearParametro("@permission_type", tipo));

                SqlParameter idSalida = new SqlParameter("@id_permission", SqlDbType.Int);
                idSalida.Direction = ParameterDirection.Output;
                parametros.Add(idSalida);

                acceso.AbrirConexionBD();
                res = acceso.EscribirBD("SP_Permissions_Insertar", parametros);

                permiso.Id = (int)idSalida.Value;

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
        /// Lista plana de todos los permisos (sin resolver la composición).
        /// Cada fila se materializa como SimplePermission o CompositePermission
        /// según permission_type, pero un CompositePermission acá NO trae sus hijos cargados.
        /// Para el árbol completo usar ObtenerArbolCompleto().
        /// </summary>
        public override List<IPermission> ListarTodo()
        {
            try
            {
                List<IPermission> permisos = new List<IPermission>();

                acceso.AbrirConexionBD();
                DataTable tb = acceso.LeerBD("SP_Permissions_ObtenerTodos");

                foreach (DataRow row in tb.Rows)
                {
                    permisos.Add(MapearFila(row));
                }

                return permisos;
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
        /// Devuelve todos los permisos, con los CompositePermission ya con sus
        /// Hijos completos (recorre permission_composition recursivamente).
        /// Ideal para poblar un TreeView de administración de permisos.
        /// </summary>
        public List<IPermission> ObtenerArbolCompleto()
        {
            List<IPermission> planos = ListarTodo();
            Dictionary<int, IPermission> mapa = planos.ToDictionary(p => p.Id, p => p);

            foreach (var permiso in planos)
            {
                if (permiso is CompositePermission compuesto)
                {
                    CargarHijosRecursivo(compuesto, mapa, new HashSet<int> { compuesto.Id });
                }
            }

            return planos;
        }

        private void CargarHijosRecursivo(CompositePermission padre, Dictionary<int, IPermission> mapa, HashSet<int> visitados)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_composite_permission", padre.Id)
            };

            acceso.AbrirConexionBD();
            DataTable tb = acceso.LeerBD("SP_PermissionComposition_ObtenerHijos", parametros);
            acceso.CerrarConexionBD();

            foreach (DataRow row in tb.Rows)
            {
                int idHijo = int.Parse(row["id_permission"].ToString());

                // corta cualquier ciclo que pudiera existir en la BD
                if (visitados.Contains(idHijo))
                    continue;

                IPermission hijo = mapa.ContainsKey(idHijo) ? mapa[idHijo] : MapearFila(row);
                padre.Hijos.Add(hijo);

                if (hijo is CompositePermission hijoCompuesto)
                {
                    var nuevosVisitados = new HashSet<int>(visitados) { idHijo };
                    CargarHijosRecursivo(hijoCompuesto, mapa, nuevosVisitados);
                }
            }
        }

        private IPermission MapearFila(DataRow row)
        {
            int id = int.Parse(row["id_permission"].ToString());
            string nombre = row["permission_name"].ToString();
            string descripcion = row["description"] == DBNull.Value ? null : row["description"].ToString();
            string tipo = row["permission_type"].ToString();

            if (tipo == "COMPOSITE")
                return new CompositePermission(id, nombre, descripcion);

            return new SimplePermission(id, nombre, descripcion);
        }

        #endregion

        #region UPDATE

        public override int Editar(IPermission permiso)
        {
            int respuesta;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_permission", permiso.Id));
                parametros.Add(acceso.CrearParametro("@permission_name", permiso.Name));
                parametros.Add(acceso.CrearParametro("@description", permiso.Description));

                acceso.AbrirConexionBD();
                respuesta = acceso.EscribirBD("SP_Permissions_Editar", parametros);

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

        /// <summary>
        /// Borra el permiso y limpia sus referencias en permission_composition y role_permissions
        /// (lo resuelve el SP para que quede en una sola transacción del lado de la BD).
        /// </summary>
        public override int Borrar(IPermission permiso)
        {
            int respuesta;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_permission", permiso.Id));

                acceso.AbrirConexionBD();
                respuesta = acceso.EscribirBD("SP_Permissions_Borrar", parametros);

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

        #region COMPOSICIÓN DEL ÁRBOL

        public int AsignarHijo(int idPermisoCompuesto, int idPermisoHijo)
        {
            int respuesta;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_composite_permission", idPermisoCompuesto));
                parametros.Add(acceso.CrearParametro("@id_child_permission", idPermisoHijo));

                acceso.AbrirConexionBD();
                respuesta = acceso.EscribirBD("SP_PermissionComposition_Agregar", parametros);

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

        public int QuitarHijo(int idPermisoCompuesto, int idPermisoHijo)
        {
            int respuesta;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_composite_permission", idPermisoCompuesto));
                parametros.Add(acceso.CrearParametro("@id_child_permission", idPermisoHijo));

                acceso.AbrirConexionBD();
                respuesta = acceso.EscribirBD("SP_PermissionComposition_Quitar", parametros);

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
