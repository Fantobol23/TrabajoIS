using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Entities.Users
{
    public class UserStatus
    {
        #region CONSTRUCTOR
        public UserStatus()
        {
            
        }

        #endregion

        #region PROPIEDADES

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string statusName;

        public string StatusName
        {
            get { return statusName; }
            set { statusName = value; }
        }

        #endregion

        public override string ToString()
        {
            return this.StatusName ?? "Sin Estado";
        }
    }
}
