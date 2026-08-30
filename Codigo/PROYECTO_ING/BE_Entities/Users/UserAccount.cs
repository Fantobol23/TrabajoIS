using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Entities.Users
{
    public class UserAccount
    {
        #region CONSTRUCTOR
        public UserAccount()
        {
            Estado = new UserStatus();
        }
        #endregion

        #region PROPIEDADES

        private int id_user;

        public int Id
        {
            get { return id_user; }
            set { id_user = value; }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string firstname;

        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        private string lastname;

        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

        private string email_opt_1;

        public string EmailOption1
        {
            get { return email_opt_1; }
            set { email_opt_1 = value; }
        }

        private string email_opt_2;

        public string EmailOption2
        {
            get { return email_opt_2; }
            set { email_opt_2 = value; }
        }

        private UserStatus estado;

        public UserStatus Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        #endregion

        public override string ToString()
        {
            return this.Username ?? "Sin nombre";
        }
    }
}
