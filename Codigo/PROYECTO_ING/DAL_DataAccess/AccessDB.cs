using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccess
{
    public class AccessDB
    {
        private SqlConnection _conexion;

        #region ABRIR/CERRAR

        public void AbrirConexionBD()
        {
            _conexion = new SqlConnection();

            _conexion.ConnectionString = ObtenerCadenaConexion();

            _conexion.Open();
        }

        /// <summary>
        /// Lee la cadena de conexión "PROYECTO_ING_DB" del App.config de UI_Presentation.
        /// Si no existe, usa la instancia local por defecto (.\SQLEXPRESS).
        /// Así cada integrante sólo cambia el App.config y no el código.
        /// </summary>
        private static string ObtenerCadenaConexion()
        {
            var cs = ConfigurationManager.ConnectionStrings["PROYECTO_ING_DB"];

            if (cs != null && !string.IsNullOrWhiteSpace(cs.ConnectionString))
                return cs.ConnectionString;

            return @"INTEGRATED SECURITY=True; DATA SOURCE=.\SQLEXPRESS; INITIAL CATALOG=PROYECTO_ING_DB";
        }

        public void CerrarConexionBD()
        {
            if (_conexion == null)
                return;

            _conexion.Close();
            _conexion = null;
            GC.Collect();
        }
        #endregion

        #region LECTURA/ESCRITURA

        public DataTable LeerBD(string sp, List<SqlParameter> parametros = null)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = CrearComando(sp, parametros);

            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            adapter = null;

            return tabla;
        }

        public int EscribirBD(string sp, List<SqlParameter> parametros = null)
        {
            SqlCommand comando = CrearComando(sp, parametros);
            int filasAfectadas = 0;

            if (parametros != null)
            {
                filasAfectadas = comando.ExecuteNonQuery();
            }
            else
            {
                filasAfectadas = -1;
            }
            return filasAfectadas;
        }
        #endregion

        #region COMANDOS/PARÁMETROS

        public SqlCommand CrearComando(string sp, List<SqlParameter>parametros = null)
        {
            SqlCommand comando = new SqlCommand(sp, _conexion);
            comando.CommandType = CommandType.StoredProcedure;

            if (parametros != null)
                comando.Parameters.AddRange(parametros.ToArray());

            return comando;
        }

        public SqlParameter CrearParametro(string nombre, int valor)
        {
            SqlParameter parametro = new SqlParameter(nombre, valor);
            parametro.DbType = DbType.Int32;
            return parametro;
        }

        public SqlParameter CrearParametro(string nombre, string valor) // STRING
        {
            SqlParameter parametro = new SqlParameter(nombre, valor);
            parametro.DbType = DbType.String;
            return parametro;
        }

        public SqlParameter CrearParametro(string nombre, bool valor) // BOOL
        {
            SqlParameter parametro = new SqlParameter(nombre, valor);
            parametro.DbType = DbType.Boolean;
            return parametro;
        }
        public SqlParameter CrearParametro(string nombre, DateTime valor) // DATE
        {
            SqlParameter parametro = new SqlParameter(nombre, valor);
            parametro.DbType = DbType.DateTime;
            return parametro;
        }

        public SqlParameter CrearParametro(string nombre, Enum valor)
        {
            SqlParameter parametro = new SqlParameter(nombre, valor);
            parametro.DbType = DbType.Int32;
            return parametro;
        }
        #endregion
        #region CONSULTAS DE TEXTO (uso interno del mecanismo genérico de dígitos verificadores)

        /// <summary>
        /// Ejecuta una consulta SQL de texto. Sólo la usa el mecanismo genérico de
        /// dígitos verificadores, cuyos nombres de tabla/columna salen de un registro
        /// interno (nunca de lo que escribe el usuario) y los valores van por parámetros.
        /// </summary>
        public DataTable LeerBDConsulta(string sql, List<SqlParameter> parametros = null)
        {
            SqlCommand comando = new SqlCommand(sql, _conexion);
            comando.CommandType = CommandType.Text;

            if (parametros != null)
                comando.Parameters.AddRange(parametros.ToArray());

            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);

            return tabla;
        }

        public int EscribirBDConsulta(string sql, List<SqlParameter> parametros = null)
        {
            SqlCommand comando = new SqlCommand(sql, _conexion);
            comando.CommandType = CommandType.Text;

            if (parametros != null)
                comando.Parameters.AddRange(parametros.ToArray());

            return comando.ExecuteNonQuery();
        }

        public SqlParameter CrearParametro(string nombre, long valor) // BIGINT
        {
            SqlParameter parametro = new SqlParameter(nombre, valor);
            parametro.DbType = DbType.Int64;
            return parametro;
        }

        public SqlParameter CrearParametroObjeto(string nombre, object valor)
        {
            return new SqlParameter(nombre, valor ?? DBNull.Value);
        }

        #endregion
    }
}
