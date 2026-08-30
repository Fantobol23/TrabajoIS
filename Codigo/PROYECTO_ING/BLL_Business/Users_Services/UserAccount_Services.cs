using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using BE_Entities;
using BE_Entities.Users;
using DAL_DataAccess;
using DAL_DataAccess.DataEntities;
using SERVICES;
using SERVICES.UserSession;

namespace BLL_Business.Users_Services
{
    public class UserAccount_Services
    {
        UserAccount_Data _userRepository;
        Hash_Service _hashService;

        #region LOGIN

        public bool LoginUser(UserAccount user_account)
        {
            bool user_valido = false;

            try
            {
                if(UserSession.SesionActiva)
                {
                    throw new Exception("Ya hay una sesión iniciada.");
                }
                else
                {
                    _userRepository = new UserAccount_Data();
                    UserAccount user_validado = new UserAccount();
                    user_validado = _userRepository.Login(user_account);


                    if (user_validado.Username != null && user_validado.Password != null)
                    {
                        UserSession.SessionLogin(user_validado);
                        user_valido = true;
                    }
                    else
                    {
                        return user_valido;
                    }
                }

                return user_valido;
            }
            catch (Exception ex)
            {
                throw new Exception("BLL_UserAccount_Services: LoginUser() /" + ex.Message);
            }
        }

        #endregion

        #region C-R-U-D

        public UserAccount CreateUser(UserAccount userAccount)
        {
            _hashService = new Hash_Service();
            userAccount.Password = _hashService.EncriptarPassword(userAccount.Password);

            userAccount.Estado = new UserStatus();
            userAccount.Estado.Id = 1;

            _userRepository = new UserAccount_Data();
            _userRepository.Insertar(userAccount);
            _userRepository = null;

            return userAccount;
        }
        public void UpdateUser(UserAccount userAccount)
        {
            _userRepository = new UserAccount_Data();

            try
            {
                if (_userRepository.Editar(userAccount) == 1)
                {
                    UserSession.GetInstance().UserAccount = userAccount;
                }
            }
            catch
            {
                throw new Exception("Error al traer la información de la base de datos");
            }
            
        }
        public List<UserAccount> GetUsersByState(UserStatus _userStatus)
        {
            try
            {
                _userRepository = new UserAccount_Data();

                List<UserAccount> users_accounts = _userRepository.ObtenerUsersPorEstado(_userStatus);

                return users_accounts;
            }
            catch
            {
                throw new Exception("Error al traer la información de la base de datos");
            }
        }
        public List<UserAccount> GetAllUsers()
        {
            try
            {
                _userRepository = new UserAccount_Data();

                List<UserAccount> users_accounts = _userRepository.ListarTodo();

                return users_accounts;
            }
            catch
            {
                throw new Exception("Error al traer la información de la base de datos");
            }
        }
        public bool GetUserByUsername(string username)
        {
            bool existe = false;

            UserAccount user_result = _userRepository.ObtenerUnoPorUsername(username);

            if (user_result != null)
            {
                existe = true;
                return existe;
            }
            else
                return existe;
        }
        #endregion

        #region C-R-U-D Específicos

        public bool ChangeUserPassword(UserAccount userAccount, string newPassword)
        {
            bool validado = false;

            _userRepository = new UserAccount_Data();
            _hashService = new Hash_Service();

            try
            {
                userAccount.Password = _hashService.EncriptarPassword(newPassword);

                if (_userRepository.CambiarPassword(userAccount) == 1)
                {
                    UserSession.GetInstance().UserAccount = userAccount;
                    validado = true;
                }
                return validado;
            }
            catch
            {
                return validado = false;
                throw new Exception("Error al modificar la contraseña del usuario.");
            }
        }
        public void DisableUser(UserAccount userAccount)
        {
            _userRepository = new UserAccount_Data();

            try
            {
                _userRepository.Deshabilitar(userAccount);
            }
            catch
            {
                throw new Exception("Error en la comunicación con la base de datos");
            }
        }
        public void EnableUser(UserAccount userAccount)
        {
            _userRepository = new UserAccount_Data();

            try
            {
                _userRepository.Habilitar(userAccount);
            }
            catch
            {
                throw new Exception("Error en la comunicación con la base de datos");
            }
        }
        
        #endregion
    }
}
