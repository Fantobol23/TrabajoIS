using System;
using System.Collections.Generic;
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
            
            _conexion.ConnectionString = @"INTEGRATED SECURITY=True; DATA SOURCE=MRCROWLY-NB\SQLEXPRESS; INITIAL CATALOG=PROYECTO_ING_DB";
            
            _conexion.Open();
        }

        public void CerrarConexionBD()
        {
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
    }
}
