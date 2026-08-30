using BE_Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Entities.ActivityLog
{
    public class ActivityLog
    {
        #region CONSTRUCTOR

        #endregion

        #region PROPIEDADES

        private int idLog;

        public int IdLog
        {
            get { return idLog; }
            set { idLog = value; }
        }

        private ActivityType tipoLog;

        public ActivityType TipoLog
        {
            get { return tipoLog; }
            set { tipoLog = value; }
        }

        private DateTime fechaLog;

        public DateTime FechaLog
        {
            get { return fechaLog; }
            set { fechaLog = value; }
        }

        private UserAccount userAccount;

        public UserAccount UserAccount
        {
            get { return userAccount; }
            set { userAccount = value; }
        }


        #endregion

    }
}
