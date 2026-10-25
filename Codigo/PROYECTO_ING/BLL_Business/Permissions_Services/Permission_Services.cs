using BE_Entities.Permissions;
using DAL_DataAccess.DataEntities;
using BLL_Business.Integrity_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Business.Permissions_Services
{
    public class Permission_Services
    {
        private readonly Permission_Data permission_data;
        private readonly DigitoVerificador_Services dv_services;

        public Permission_Services()
        {
            permission_data = new Permission_Data();
            dv_services = new DigitoVerificador_Services();
        }

        #region CONSTRUCCIÓN EN MEMORIA

        public IPermission CrearPermisoSimple(string nombre, string descripcion = null)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El permiso debe tener un nombre.");

            return new SimplePermission(0, nombre, descripcion);
        }

        public IPermission CrearPermisoCompuesto(string nombre, string descripcion, params IPermission[] hijos)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El permiso debe tener un nombre.");

            var compuesto = new CompositePermission(0, nombre, descripcion);

            foreach (var hijo in hijos ?? Array.Empty<IPermission>())
            {
                compuesto.Agregar(hijo);
            }

            return compuesto;
        }

        public List<IPermission> Expandir(IPermission permiso)
        {
            return permiso.ObtenerPermisosSimples();
        }

        public bool Otorga(IPermission permiso, string nombrePermiso)
        {
            return permiso.Contiene(nombrePermiso);
        }

        #endregion

        #region PERSISTENCIA

        public int Guardar(IPermission permiso)
        {
            int resultado = permission_data.Insertar(permiso);

            if (permiso is CompositePermission compuesto)
            {
                foreach (var hijo in compuesto.Hijos.Where(h => h.Id > 0))
                {
                    AsignarHijo(compuesto.Id, hijo.Id);
                }
            }

            return resultado;
        }

        public int Editar(IPermission permiso) => permission_data.Editar(permiso);

        /// <summary>El SP también borra sus filas en permission_composition y role_permissions: se ajustan sus DV.</summary>
        public int Borrar(IPermission permiso) => dv_services.Ejecutar(() => permission_data.Borrar(permiso),
            new AlcanceDV(TablasVerificables.COMPOSICION_PERMISOS, "id_composite_permission", permiso.Id),
            new AlcanceDV(TablasVerificables.COMPOSICION_PERMISOS, "id_child_permission", permiso.Id),
            new AlcanceDV(TablasVerificables.ROL_PERMISOS, "id_permission", permiso.Id));

        public List<IPermission> ListarTodo() => permission_data.ListarTodo();

        public List<IPermission> ObtenerArbolCompleto() => permission_data.ObtenerArbolCompleto();

        /// <summary>
        /// Agrega un hijo a un permiso compuesto. Antes de tocar la BD valida en memoria
        /// (con el árbol completo) que no se forme un ciclo: A ⊃ B ⊃ A.
        /// </summary>
        public int AsignarHijo(int idPermisoCompuesto, int idPermisoHijo)
        {
            var arbol = ObtenerArbolCompleto();
            var padre = arbol.FirstOrDefault(p => p.Id == idPermisoCompuesto) as CompositePermission;
            var hijo = arbol.FirstOrDefault(p => p.Id == idPermisoHijo);

            if (padre == null)
                throw new InvalidOperationException("Sólo se pueden agregar hijos a un permiso compuesto.");
            if (hijo == null)
                throw new InvalidOperationException("El permiso a agregar no existe.");
            if (padre.Hijos.Any(h => h.Id == idPermisoHijo))
                throw new InvalidOperationException("El permiso ya forma parte del grupo.");

            padre.Agregar(hijo); // lanza InvalidOperationException si generaría un ciclo

            return dv_services.Ejecutar(() => permission_data.AsignarHijo(idPermisoCompuesto, idPermisoHijo),
                AlcanceComposicion(idPermisoCompuesto, idPermisoHijo));
        }

        public int QuitarHijo(int idPermisoCompuesto, int idPermisoHijo)
            => dv_services.Ejecutar(() => permission_data.QuitarHijo(idPermisoCompuesto, idPermisoHijo),
                AlcanceComposicion(idPermisoCompuesto, idPermisoHijo));

        private static AlcanceDV AlcanceComposicion(int idPadre, int idHijo)
        {
            return new AlcanceDV(TablasVerificables.COMPOSICION_PERMISOS,
                new Dictionary<string, object> { { "id_composite_permission", idPadre }, { "id_child_permission", idHijo } });
        }

        /// <summary>
        /// Raíces del árbol: permisos que no son hijos de ningún compuesto.
        /// Son los nodos de primer nivel del TreeView; el resto se agrega recursivamente.
        /// </summary>
        public List<IPermission> ObtenerRaices()
        {
            var arbol = ObtenerArbolCompleto();
            var idsHijos = new HashSet<int>(arbol.OfType<CompositePermission>().SelectMany(c => c.Hijos).Select(h => h.Id));

            return arbol.Where(p => !idsHijos.Contains(p.Id)).ToList();
        }

        #endregion
    }
}
