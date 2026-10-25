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
using BLL_Business.Integrity_Services;
using BLL_Business.Role_Services;

namespace BLL_Business.Users_Services
{
    public class UserAccount_Services
    {
        UserAccount_Data _userRepository;
        Hash_Service _hashService;
        readonly DigitoVerificador_Services _dvServices = new DigitoVerificador_Services();

        /// <summary>
        /// Registro(s) de usersaccounts que toca una operación: por id si ya existe,
        /// por username si es un alta (todavía no tiene id).
        /// </summary>
        private static AlcanceDV AlcanceUsuario(UserAccount user)
        {
            return user.Id > 0
                ? new AlcanceDV(TablasVerificables.USUARIOS, "id_user", user.Id)
                : new AlcanceDV(TablasVerificables.USUARIOS, "username", user.Username);
        }

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
                        user_validado.Roles = CargarRoles(user_validado.Id);
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

        /// <summary>
        /// Roles del usuario con su árbol de permisos (Composite). Si las tablas de
        /// permisos todavía no existen, el login sigue funcionando sin roles.
        /// </summary>
        private List<Role> CargarRoles(int idUsuario)
        {
            try
            {
                return new Role_Services.Role_Services().ObtenerRolesPorUsuario(idUsuario);
            }
            catch
            {
                return new List<Role>();
            }
        }

        /// <summary>
        /// Valida usuario/contraseña SIN iniciar sesión y verifica que tenga el permiso pedido.
        /// Se usa en la pantalla de integridad, que aparece antes del login.
        /// </summary>
        public UserAccount ValidarCredencialesConPermiso(string username, string passwordPlano, string permiso)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(passwordPlano))
                return null;

            _hashService = new Hash_Service();
            _userRepository = new UserAccount_Data();

            var credenciales = new UserAccount { Username = username, Password = _hashService.EncriptarPassword(passwordPlano) };
            UserAccount user = _userRepository.Login(credenciales);

            if (user == null || user.Username == null || user.Id <= 0)
                return null;

            user.Roles = CargarRoles(user.Id);

            return user.TienePermiso(permiso) ? user : null;
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
            _dvServices.Ejecutar(() => _userRepository.Insertar(userAccount), AlcanceUsuario(userAccount));
            _userRepository = null;

            return userAccount;
        }
        public void UpdateUser(UserAccount userAccount)
        {
            _userRepository = new UserAccount_Data();

            try
            {
                if (_dvServices.Ejecutar(() => _userRepository.Editar(userAccount), AlcanceUsuario(userAccount)) == 1)
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

            if (_userRepository == null)
                _userRepository = new UserAccount_Data();

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

                if (_dvServices.Ejecutar(() => _userRepository.CambiarPassword(userAccount), AlcanceUsuario(userAccount)) == 1)
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
                _dvServices.Ejecutar(() => _userRepository.Deshabilitar(userAccount), AlcanceUsuario(userAccount));
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
                _dvServices.Ejecutar(() => _userRepository.Habilitar(userAccount), AlcanceUsuario(userAccount));
            }
            catch
            {
                throw new Exception("Error en la comunicación con la base de datos");
            }
        }
        
        #endregion
    }
}
