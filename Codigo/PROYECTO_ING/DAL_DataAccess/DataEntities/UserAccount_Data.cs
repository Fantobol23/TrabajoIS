using BE_Entities;
using BE_Entities.Users;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccess.DataEntities
{
    public class UserAccount_Data : Mapper<UserAccount>
    {
        public UserAccount_Data()
        {
            acceso = new AccessDB();
        }

        #region CREATE

        public override int Insertar(UserAccount user_account)
        {
            int res;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@username", user_account.Username));
                parametros.Add(acceso.CrearParametro("@userpassword", user_account.Password));
                parametros.Add(acceso.CrearParametro("@firstname", user_account.FirstName));
                parametros.Add(acceso.CrearParametro("@lastname", user_account.Lastname));
                parametros.Add(acceso.CrearParametro("@email_opt_1", user_account.EmailOption1));
                parametros.Add(acceso.CrearParametro("@email_opt_2", user_account.EmailOption2));
                parametros.Add(acceso.CrearParametro("@id_user_status", user_account.Estado.Id));

                acceso.AbrirConexionBD();
                res = acceso.EscribirBD("SP_UsersAccounts_Insertar", parametros);
                acceso.CerrarConexionBD();

                return res;
            }
            catch
            {
                return res = -1;
                throw new NotImplementedException();
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion

        #region READ

        public UserAccount Login(UserAccount user)
        {
            UserAccount userLogged = new UserAccount();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@username", user.Username));
            parametros.Add(acceso.CrearParametro("@userpassword", user.Password));

            try
            {
                acceso.AbrirConexionBD();
                DataTable tb = acceso.LeerBD("SP_UsersAccounts_Login", parametros);

                if(tb.Rows.Count > 0)
                {
                    DataRow data = tb.Rows[0];
                    userLogged.Id = int.Parse(data["id_user"].ToString());
                    userLogged.Username = data["username"].ToString();
                    userLogged.Password = data["userpassword"].ToString();
                    userLogged.FirstName = data["firstname"].ToString();
                    userLogged.Lastname = data["lastname"].ToString();
                    userLogged.EmailOption1 = data["email_opt_1"].ToString();
                    userLogged.EmailOption2 = data["email_opt_2"].ToString();
                    userLogged.Estado = new UserStatus();
                    userLogged.Estado.Id = int.Parse(data["id_user_status"].ToString());
                    userLogged.Estado.StatusName = data["status_name"].ToString();
                }
                return userLogged;
            }  
            catch
            {
                throw new Exception("Usuario no encontrado");
            }
            finally
            {
                acceso.CerrarConexionBD();
            }
        }
        public override List<UserAccount> ListarTodo()
        {
            try
            {
                List<UserAccount> users_accounts = new List<UserAccount>();

                acceso.AbrirConexionBD();
                DataTable tb = acceso.LeerBD("SP_UsersAccounts_ObtenerTodos");

                foreach (DataRow row in tb.Rows)
                {
                    UserAccount user_account = new UserAccount();
                    user_account.Id = int.Parse(row["id_user"].ToString());
                    user_account.Username = row["username"].ToString();
                    user_account.Password = row["userpassword"].ToString();
                    user_account.FirstName = row["firstname"].ToString();
                    user_account.Lastname = row["lastname"].ToString();
                    user_account.EmailOption1 = row["email_opt_1"].ToString();
                    user_account.EmailOption2 = row["email_opt_2"].ToString();
                    user_account.Estado.Id = int.Parse(row["id_user_status"].ToString());
                    user_account.Estado.StatusName = row["status_name"].ToString();

                    users_accounts.Add(user_account);
                }
                return users_accounts;
            }
            catch
            {
                throw new NotImplementedException();
            }
            finally
            {
                acceso.CerrarConexionBD();
                GC.Collect();
            }
        }
        public List<UserAccount> ObtenerUsersPorEstado(UserStatus _userStatus)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            List<UserAccount> users_encontrados = new List<UserAccount>();

            parametros.Add(acceso.CrearParametro("@id_user_status", _userStatus.Id));

            try
            {
                acceso.AbrirConexionBD();
                DataTable tb = acceso.LeerBD("SP_UsersAccounts_ObtenerPorEstado", parametros);
                acceso.CerrarConexionBD();

                foreach (DataRow row in tb.Rows)
                {
                    UserAccount user_account = new UserAccount();
                    user_account.Id = int.Parse(row["id_user"].ToString());
                    user_account.Username = row["username"].ToString();
                    user_account.FirstName = row["firstname"].ToString();
                    user_account.Lastname = row["lastname"].ToString();
                    user_account.EmailOption1 = row["email_opt_1"].ToString();
                    user_account.EmailOption2 = row["email_opt_2"].ToString();
                    user_account.Estado.Id = int.Parse(row["id_user_status"].ToString());

                    users_encontrados.Add(user_account);
                }
                return users_encontrados;
            }
            catch
            {
                throw new Exception("ERROR BD - No se pudieron obtener los registros");
            }
        }
        public UserAccount ObtenerUnoPorUsername(string username)
        {
            UserAccount user_found = null;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@username", username));

                acceso.AbrirConexionBD();
                DataTable tb = acceso.LeerBD("SP_UsersAccounts_ObtenerPorUsername", parametros);

                if(tb.Rows.Count > 0)
                {
                    user_found = new UserAccount();
                    DataRow data = tb.Rows[0];

                    user_found.Username = data["username"].ToString();
                    return user_found;
                }
                return user_found;
            }
            catch
            {
                throw new Exception("Error al leer la base de datos");
            }
        }

        #endregion

        #region UPDATE
        public override int Editar(UserAccount user_account)
        {
            int respuesta = 0;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@username", user_account.Username));
                parametros.Add(acceso.CrearParametro("@pass", user_account.Password));
                parametros.Add(acceso.CrearParametro("@firstname", user_account.FirstName));
                parametros.Add(acceso.CrearParametro("@lastname", user_account.Lastname));
                parametros.Add(acceso.CrearParametro("@email_opt_1", user_account.EmailOption1));
                parametros.Add(acceso.CrearParametro("@email_opt_2", user_account.EmailOption2));

                acceso.AbrirConexionBD();
                respuesta = acceso.EscribirBD("SP_UsersAccounts_ModificarPerfil", parametros);

                return respuesta;
            }
            catch
            {
                return respuesta = -1;
                throw new NotImplementedException();
            }
            finally
            {
                acceso.CerrarConexionBD();
                GC.Collect();
            }
        }
        public int CambiarPassword(UserAccount user_account)
        {
            int respuesta = 0;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@username", user_account.Username));
                parametros.Add(acceso.CrearParametro("@newpass", user_account.Password));

                acceso.AbrirConexionBD();
                respuesta = acceso.EscribirBD("SP_UsersAccounts_CambiarPassword", parametros);
                return respuesta;
            }
            catch
            {
                return respuesta = -1;
                throw new NotImplementedException();
            }
            finally
            {
                acceso.CerrarConexionBD();
                GC.Collect();
            }
        }
        public int Deshabilitar(UserAccount user_account)
        {
            int respuesta = 0;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@username", user_account.Username));
                parametros.Add(acceso.CrearParametro("@id_user_status", user_account.Estado.Id));

                acceso.AbrirConexionBD();
                respuesta = acceso.EscribirBD("SP_UsersAccounts_Deshabilitar", parametros);
            }
            catch
            {
                return respuesta = -1;
                throw new NotImplementedException();
            }
            finally
            {
                acceso.CerrarConexionBD();
                GC.Collect();
            }
            return respuesta;
        }
        public int Habilitar(UserAccount user_account)
        {
            int respuesta = 0;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@username", user_account.Username));
                parametros.Add(acceso.CrearParametro("@id_user_status", user_account.Estado.Id));

                acceso.AbrirConexionBD();
                respuesta = acceso.EscribirBD("SP_UsersAccounts_Habilitar", parametros);
            }
            catch
            {
                return respuesta = -1;
                throw new NotImplementedException();
            }
            finally
            {
                acceso.CerrarConexionBD();
                GC.Collect();
            }
            return respuesta;
        }
        #endregion

        #region DELETE
        public override int Borrar(UserAccount user_account)
        {
            return 0;
        }

        #endregion
    }
}
