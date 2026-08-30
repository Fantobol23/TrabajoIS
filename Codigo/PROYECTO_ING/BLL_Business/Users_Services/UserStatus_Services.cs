using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_Entities;
using BE_Entities.Users;
using DAL_DataAccess;
using DAL_DataAccess.DataEntities;

namespace BLL_Business.Users_Services
{
    public class UserStatus_Services
    {
        UserStatus_Data _usersStatusRepository;

        #region FUNCIONES/METODOS

        public List<UserStatus> GetAllUsersStatus()
        {
            try
            {
                List<UserStatus> users_status = new List<UserStatus>();

                _usersStatusRepository = new UserStatus_Data();
                users_status = _usersStatusRepository.ListarTodo();

                return users_status;
            }
            catch
            {
                throw new Exception("Error al traer la información de la base de datos");
            }
        }

        #endregion
    }
}
