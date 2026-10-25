using BE_Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Entities.Users
{
    public class Role
    {
        #region CONSTRUCTOR

        public Role()
        {
            permisos = new List<IPermission>();
        }

        public Role(int id, string name, string description = null) : this()
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

        private List<IPermission> permisos;

        public List<IPermission> Permisos
        {
            get { return permisos; }
        }

        #endregion

        #region GESTIÓN DE PERMISOS

        public void AsignarPermiso(IPermission permiso)
        {
            if (permiso == null)
                throw new ArgumentNullException(nameof(permiso));

            if (!permisos.Contains(permiso))
                permisos.Add(permiso);
        }

        public void QuitarPermiso(IPermission permiso)
        {
            permisos.Remove(permiso);
        }

        #endregion

        public bool TienePermiso(string nombrePermiso)
        {
            return permisos.Any(p => p.Contiene(nombrePermiso));
        }

        public List<IPermission> ObtenerPermisosEfectivos()
        {
            var resultado = new List<IPermission>();

            foreach (var permiso in permisos)
            {
                resultado.AddRange(permiso.ObtenerPermisosSimples());
            }

            return resultado
                .GroupBy(p => p.Id)
                .Select(g => g.First())
                .ToList();
        }

        public override string ToString()
        {
            return this.Name ?? "Rol sin nombre";
        }
    }
}
