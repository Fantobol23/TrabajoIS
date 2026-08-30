using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Entities.ActivityLog
{
    public enum ActivityType
    {
        /*
        NUMERACIÓN PARA CATEGORIAS DE LOG
            100. Autenticación
            200. CRUD
            300. Configuración
            400. Sistema
            500. Administración
        */

        // 100. Autenticación
        Login = 101,
        LoginFailed = 102,
        Logout = 103,
        LogoutFailed = 104,

        // 200. CRUD
        CreateUser = 201,
        CreateUserFailed = 202,
        UpdateUser = 203,
        UpdateUserFailed = 204,
        UpdateUserPassword = 205,
        UpdateUserPasswordFailed = 206,
        UserEnabled = 207,
        UserDisabled = 208,
        UserLocked = 209,
        ChangeProfileInfo = 210,
        ChangeProfileInfoFailed = 211,
        ChangePassword = 212,
        ChangePasswordFailed = 213,

        //400. Sistema
        ReadActivityLog = 401
    }
}
