using BE_Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES.UserSession
{
    public class UserSession
    {
        private static UserSession _userSession;
        public static bool SesionActiva
        {
            get
            {
                return _userSession != null;
            }
        }
        #region CONSTRUCTOR
        private UserSession(){}

        #endregion

        #region PROPIEDADES
        
        private UserAccount userAccount;

        public UserAccount UserAccount
        {
            get { return userAccount; }
            set { userAccount = value; }
        }

        #endregion

        #region FUNCIONES/MÉTODOS

        public static void SessionLogin(UserAccount user)
        {
            if(_userSession == null)
            {
                _userSession = new UserSession();
                _userSession.UserAccount = user;
            }
            else
            {
                throw new Exception("Sesión ya iniciada.");
            }
        }

        public static void SessionLogout()
        {
            if (_userSession != null)
                _userSession = null;
            else
                throw new Exception("Sesión no iniciada");
            
        }
        public static UserSession GetInstance()
        {
            if (_userSession != null)
            {
                return _userSession;
            }
            else
            {
                throw new Exception("No hay una sesión activa");
            }
        }
        #endregion
    }
}
