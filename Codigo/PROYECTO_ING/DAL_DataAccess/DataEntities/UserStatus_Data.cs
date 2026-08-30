using BE_Entities.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccess.DataEntities
{
    public class UserStatus_Data : Mapper<UserStatus>
    {
        #region CONSTRUCTOR

        public UserStatus_Data()
        {
            acceso = new AccessDB();
        }

        #endregion

        #region FUNCIONES/METODOS
        public override int Insertar(UserStatus obj)
        {
            throw new NotImplementedException();
        }
        public override int Editar(UserStatus obj)
        {
            throw new NotImplementedException();
        }

        public override int Borrar(UserStatus obj)
        {
            throw new NotImplementedException();
        }

        public override List<UserStatus> ListarTodo()
        {
            try
            {
                List<UserStatus> status = new List<UserStatus>();

                acceso.AbrirConexionBD();
                DataTable tb = acceso.LeerBD("SP_UsersStatus_ObtenerTodos");

                foreach (DataRow row in tb.Rows)
                {
                    UserStatus user_status = new UserStatus();
                    user_status.Id = int.Parse(row["id_status"].ToString());
                    user_status.StatusName = row["status_name"].ToString();

                    status.Add(user_status);
                }
                return status;
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
        #endregion
    }
}
