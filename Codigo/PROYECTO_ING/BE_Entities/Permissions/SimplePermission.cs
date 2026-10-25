using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Entities.Permissions
{
    public class SimplePermission : IPermission
    {
        #region CONSTRUCTOR

        public SimplePermission()
        {
        }

        public SimplePermission(int id, string name, string description = null)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        #endregion

        #region PROPIEDADES

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        #endregion

        public bool Contiene(string nombrePermiso)
        {
            return string.Equals(this.name, nombrePermiso, StringComparison.OrdinalIgnoreCase);
        }

        public List<IPermission> ObtenerPermisosSimples()
        {
            return new List<IPermission> { this };
        }

        public override string ToString()
        {
            return this.Name ?? "Permiso sin nombre";
        }
    }
}
