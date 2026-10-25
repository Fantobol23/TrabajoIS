using BE_Entities.Languages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL_DataAccess.DataEntities
{
    public class Idioma_Data : Mapper<Idioma>
    {
        #region CONSTRUCTOR

        public Idioma_Data()
        {
            acceso = new AccessDB();
        }

        #endregion

        #region IDIOMAS (CRUD)

        public override int Insertar(Idioma idioma)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@language_name", idioma.Nombre));
                parametros.Add(acceso.CrearParametro("@culture_code", idioma.Codigo));

                SqlParameter idSalida = new SqlParameter("@id_language", SqlDbType.Int);
                idSalida.Direction = ParameterDirection.Output;
                parametros.Add(idSalida);

                acceso.AbrirConexionBD();
                int res = acceso.EscribirBD("SP_Languages_Insertar", parametros);

                idioma.Id = (int)idSalida.Value;
                return res;
            }
            catch
            {
                return -1;
            }
            finally
            {
                acceso.CerrarConexionBD();
            }
        }

        public override int Editar(Idioma idioma)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_language", idioma.Id));
                parametros.Add(acceso.CrearParametro("@language_name", idioma.Nombre));
                parametros.Add(acceso.CrearParametro("@culture_code", idioma.Codigo));

                acceso.AbrirConexionBD();
                return acceso.EscribirBD("SP_Languages_Editar", parametros);
            }
            catch
            {
                return -1;
            }
            finally
            {
                acceso.CerrarConexionBD();
            }
        }

        /// <summary>Borra el idioma y sus traducciones (el SP no permite borrar el idioma por defecto).</summary>
        public override int Borrar(Idioma idioma)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_language", idioma.Id));

                acceso.AbrirConexionBD();
                return acceso.EscribirBD("SP_Languages_Borrar", parametros);
            }
            catch
            {
                return -1;
            }
            finally
            {
                acceso.CerrarConexionBD();
            }
        }

        public override List<Idioma> ListarTodo()
        {
            try
            {
                List<Idioma> idiomas = new List<Idioma>();

                acceso.AbrirConexionBD();
                DataTable tb = acceso.LeerBD("SP_Languages_ObtenerTodos");

                foreach (DataRow row in tb.Rows)
                {
                    idiomas.Add(new Idioma(
                        int.Parse(row["id_language"].ToString()),
                        row["language_name"].ToString(),
                        row["culture_code"].ToString(),
                        Convert.ToBoolean(row["is_default"])));
                }

                return idiomas;
            }
            catch
            {
                throw new Exception("Error al leer los idiomas de la base de datos");
            }
            finally
            {
                acceso.CerrarConexionBD();
            }
        }

        #endregion

        #region ETIQUETAS

        public List<Etiqueta> ListarEtiquetas()
        {
            try
            {
                List<Etiqueta> etiquetas = new List<Etiqueta>();

                acceso.AbrirConexionBD();
                DataTable tb = acceso.LeerBD("SP_Labels_ObtenerTodas");

                foreach (DataRow row in tb.Rows)
                {
                    etiquetas.Add(new Etiqueta(
                        int.Parse(row["id_label"].ToString()),
                        row["label_key"].ToString(),
                        row["default_text"] == DBNull.Value ? string.Empty : row["default_text"].ToString()));
                }

                return etiquetas;
            }
            catch
            {
                throw new Exception("Error al leer las leyendas de la base de datos");
            }
            finally
            {
                acceso.CerrarConexionBD();
            }
        }

        /// <summary>Da de alta la etiqueta si no existe (el SP lo resuelve con IF NOT EXISTS).</summary>
        public int RegistrarEtiqueta(string clave, string textoOriginal)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@label_key", clave));
                parametros.Add(acceso.CrearParametro("@default_text", textoOriginal ?? string.Empty));

                acceso.AbrirConexionBD();
                return acceso.EscribirBD("SP_Labels_Registrar", parametros);
            }
            catch
            {
                return -1;
            }
            finally
            {
                acceso.CerrarConexionBD();
            }
        }

        #endregion

        #region TRADUCCIONES

        /// <summary>Diccionario clave → texto traducido para el idioma indicado.</summary>
        public Dictionary<string, string> ObtenerTraducciones(int idIdioma)
        {
            try
            {
                var resultado = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_language", idIdioma)
                };

                acceso.AbrirConexionBD();
                DataTable tb = acceso.LeerBD("SP_Translations_ObtenerPorIdioma", parametros);

                foreach (DataRow row in tb.Rows)
                {
                    resultado[row["label_key"].ToString()] = row["translated_text"].ToString();
                }

                return resultado;
            }
            catch
            {
                throw new Exception("Error al leer las traducciones de la base de datos");
            }
            finally
            {
                acceso.CerrarConexionBD();
            }
        }

        /// <summary>Inserta o actualiza la traducción (texto vacío = se borra y vuelve al texto original).</summary>
        public int GuardarTraduccion(int idIdioma, string clave, string texto)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_language", idIdioma));
                parametros.Add(acceso.CrearParametro("@label_key", clave));
                parametros.Add(acceso.CrearParametro("@translated_text", texto ?? string.Empty));

                acceso.AbrirConexionBD();
                return acceso.EscribirBD("SP_Translations_Guardar", parametros);
            }
            catch
            {
                return -1;
            }
            finally
            {
                acceso.CerrarConexionBD();
            }
        }

        #endregion
    }
}
