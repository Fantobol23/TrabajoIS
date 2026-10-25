using BE_Entities.Integrity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace DAL_DataAccess.DataEntities
{
    /// <summary>
    /// Acceso a datos GENÉRICO para los dígitos verificadores: trabaja sobre cualquier
    /// tabla descripta por un TablaVerificable. Las sentencias se arman con los nombres
    /// de tabla/columna del registro interno (validados) y los valores van parametrizados.
    /// El DVV se guarda en la entidad adicional "dvv" mediante SPs.
    /// </summary>
    public class DigitoVerificador_Data
    {
        private readonly AccessDB acceso;
        private static readonly Regex IDENTIFICADOR_VALIDO = new Regex("^[A-Za-z_][A-Za-z0-9_]*$");

        public DigitoVerificador_Data()
        {
            acceso = new AccessDB();
        }

        #region LECTURA DE REGISTROS

        /// <summary>Todos los registros de la tabla (clave + datos + dvh), ordenados por clave.</summary>
        public DataTable ObtenerRegistros(TablaVerificable tabla)
        {
            return ObtenerRegistros(tabla, null);
        }

        /// <summary>Registros de la tabla que cumplen el filtro (columna = valor, unidos con AND).</summary>
        public DataTable ObtenerRegistros(TablaVerificable tabla, IDictionary<string, object> filtro)
        {
            List<SqlParameter> parametros;
            string sql = "SELECT " + string.Join(", ", tabla.ColumnasALeer.Select(Q)) +
                         " FROM " + Q(tabla.Nombre) +
                         ArmarWhere(filtro, out parametros) +
                         " ORDER BY " + string.Join(", ", tabla.ColumnasClave.Select(Q));

            try
            {
                acceso.AbrirConexionBD();
                return acceso.LeerBDConsulta(sql, parametros);
            }
            finally
            {
                acceso.CerrarConexionBD();
            }
        }

        #endregion

        #region DVH

        /// <summary>Guarda el DVH de UN registro, identificado por los valores de su clave.</summary>
        public int GuardarDVH(TablaVerificable tabla, IDictionary<string, object> clave, long dvh)
        {
            List<SqlParameter> parametros;
            string sql = "UPDATE " + Q(tabla.Nombre) +
                         " SET " + Q(tabla.ColumnaDVH) + " = @dvh_valor" +
                         ArmarWhere(clave, out parametros);

            parametros.Add(acceso.CrearParametro("@dvh_valor", dvh));

            try
            {
                acceso.AbrirConexionBD();
                return acceso.EscribirBDConsulta(sql, parametros);
            }
            finally
            {
                acceso.CerrarConexionBD();
            }
        }

        #endregion

        #region DVV (entidad "dvv")

        /// <summary>Devuelve el DVV guardado de la tabla, o null si no existe.</summary>
        public long? ObtenerDVV(string nombreTabla)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@table_name", nombreTabla)
                };

                acceso.AbrirConexionBD();
                DataTable tb = acceso.LeerBD("SP_DVV_Obtener", parametros);

                if (tb.Rows.Count == 0 || tb.Rows[0]["dvv_value"] == DBNull.Value)
                    return null;

                return Convert.ToInt64(tb.Rows[0]["dvv_value"]);
            }
            finally
            {
                acceso.CerrarConexionBD();
            }
        }

        /// <summary>Inserta o actualiza el DVV de la tabla.</summary>
        public int GuardarDVV(string nombreTabla, long dvv)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@table_name", nombreTabla),
                    acceso.CrearParametro("@dvv_value", dvv)
                };

                acceso.AbrirConexionBD();
                return acceso.EscribirBD("SP_DVV_Guardar", parametros);
            }
            finally
            {
                acceso.CerrarConexionBD();
            }
        }

        #endregion

        #region AUXILIARES

        private string ArmarWhere(IDictionary<string, object> filtro, out List<SqlParameter> parametros)
        {
            parametros = new List<SqlParameter>();

            if (filtro == null || filtro.Count == 0)
                return string.Empty;

            var condiciones = new List<string>();
            int i = 0;

            foreach (var par in filtro)
            {
                string nombreParametro = "@f" + i++;
                condiciones.Add(Q(par.Key) + " = " + nombreParametro);
                parametros.Add(acceso.CrearParametroObjeto(nombreParametro, par.Value));
            }

            return " WHERE " + string.Join(" AND ", condiciones);
        }

        /// <summary>Valida el identificador y lo encierra entre corchetes.</summary>
        private static string Q(string identificador)
        {
            if (identificador == null || !IDENTIFICADOR_VALIDO.IsMatch(identificador))
                throw new ArgumentException("Identificador SQL inválido: " + identificador);

            return "[" + identificador + "]";
        }

        #endregion
    }
}
