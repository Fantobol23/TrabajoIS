using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            roles = new List<Role>();
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

        private List<Role> roles;

        /// <summary>
        /// Roles del usuario (con su árbol de permisos Composite ya resuelto).
        /// Se cargan una sola vez en el login. [Browsable(false)] evita que aparezca
        /// como columna en los DataGridView que listan usuarios.
        /// </summary>
        [Browsable(false)]
        public List<Role> Roles
        {
            get { return roles; }
            set { roles = value ?? new List<Role>(); }
        }

        /// <summary>True si alguno de sus roles otorga el permiso (búsqueda recursiva en el árbol).</summary>
        public bool TienePermiso(string nombrePermiso)
        {
            foreach (var rol in roles)
            {
                if (rol.TienePermiso(nombrePermiso))
                    return true;
            }
            return false;
        }
        #endregion

        public override string ToString()
        {
            return this.Username ?? "Sin nombre";
        }
    }
}
