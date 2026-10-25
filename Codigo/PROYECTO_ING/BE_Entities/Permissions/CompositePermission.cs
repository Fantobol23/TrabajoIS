using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Entities.Permissions
{
    public class CompositePermission : IPermission
    {
        #region CONSTRUCTOR

        public CompositePermission()
        {
            hijos = new List<IPermission>();
        }

        public CompositePermission(int id, string name, string description = null) : this()
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

        private List<IPermission> hijos;

        /// <summary>
        /// Permisos (simples o compuestos) que forman parte de este grupo.
        /// </summary>
        public List<IPermission> Hijos
        {
            get { return hijos; }
        }

        #endregion

        #region GESTIÓN DE HIJOS

        public void Agregar(IPermission permiso)
        {
            if (permiso == null)
                throw new ArgumentNullException(nameof(permiso));

            if (permiso == this)
                throw new InvalidOperationException("Un permiso compuesto no puede contenerse a sí mismo.");

            if (CausariaCiclo(permiso))
                throw new InvalidOperationException("No se puede agregar este permiso: generaría una referencia circular.");

            if (!hijos.Contains(permiso))
                hijos.Add(permiso);
        }

        public void Quitar(IPermission permiso)
        {
            hijos.Remove(permiso);
        }

        private bool CausariaCiclo(IPermission candidato)
        {
            if (candidato is CompositePermission compositoCandidato)
            {
                if (compositoCandidato == this)
                    return true;

                foreach (var nieto in compositoCandidato.Hijos)
                {
                    if (nieto == this)
                        return true;

                    if (nieto is CompositePermission compuestoNieto && compuestoNieto.CausariaCiclo(this))
                        return true;
                }
            }
            return false;
        }

        #endregion

        public bool Contiene(string nombrePermiso)
        {
            if (string.Equals(this.name, nombrePermiso, StringComparison.OrdinalIgnoreCase))
                return true;

            foreach (var hijo in hijos)
            {
                if (hijo.Contiene(nombrePermiso))
                    return true;
            }
            return false;
        }

        public List<IPermission> ObtenerPermisosSimples()
        {
            var resultado = new List<IPermission>();

            foreach (var hijo in hijos)
            {
                resultado.AddRange(hijo.ObtenerPermisosSimples());
            }

            // evita duplicados si dos ramas comparten el mismo permiso simple
            return resultado
                .GroupBy(p => p.Id)
                .Select(g => g.First())
                .ToList();
        }

        public override string ToString()
        {
            return this.Name ?? "Grupo de permisos sin nombre";
        }
    }
}
