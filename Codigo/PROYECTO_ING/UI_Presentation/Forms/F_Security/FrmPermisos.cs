using BE_Entities.Permissions;
using BE_Entities.Users;
using BLL_Business.Permissions_Services;
using BLL_Business.Role_Services;
using BLL_Business.Users_Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UI_Presentation.Languages;

namespace UI_Presentation.Forms.F_Security
{
    /// <summary>
    /// Administración de permisos con patrón COMPOSITE:
    ///  - Árbol completo de permisos (simples = hojas, compuestos = grupos).
    ///  - Roles y los permisos que tiene cada uno.
    ///  - Usuarios y sus roles (con el árbol de permisos efectivo).
    /// Los tres TreeView se llenan con la MISMA función recursiva CrearNodo().
    /// </summary>
    public partial class FrmPermisos : Form
    {
        private readonly Permission_Services _permisos = new Permission_Services();
        private readonly Role_Services _roles = new Role_Services();
        private readonly UserAccount_Services _usuarios = new UserAccount_Services();

        public FrmPermisos()
        {
            InitializeComponent();
            TraductorFormulario.Registrar(this);
        }

        #region LOAD/CARGA INICIAL

        private void FrmPermisos_Load(object sender, EventArgs e)
        {
            try
            {
                rbSimple.Checked = true;
                CargarArbolPermisos();
                CargarRoles();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        #endregion

        #region RECURSIVIDAD: IPermission → TreeNode

        /// <summary>
        /// FUNCIÓN RECURSIVA. Crea el nodo del permiso y, si es compuesto, se llama a sí
        /// misma por cada hijo. Caso base: permiso simple (hoja) o compuesto sin hijos.
        /// El parámetro "camino" corta la recursión si la BD tuviera un ciclo.
        /// </summary>
        private TreeNode CrearNodo(IPermission permiso, HashSet<int> camino = null)
        {
            camino = camino ?? new HashSet<int>();

            var nodo = new TreeNode(permiso.Name)
            {
                Tag = permiso,
                ToolTipText = permiso.Description ?? string.Empty
            };

            if (permiso is CompositePermission compuesto)
            {
                nodo.NodeFont = new Font(tvPermisos.Font, FontStyle.Bold);
                nodo.ForeColor = Color.DarkBlue;

                if (!camino.Add(compuesto.Id))
                    return nodo; // ciclo: no se sigue bajando

                foreach (IPermission hijo in compuesto.Hijos)
                {
                    nodo.Nodes.Add(CrearNodo(hijo, new HashSet<int>(camino)));
                }
            }

            return nodo;
        }

        /// <summary>Carga una lista de permisos como raíces de un TreeView (cada una con su subárbol).</summary>
        private void LlenarArbol(TreeView arbol, IEnumerable<IPermission> raices)
        {
            arbol.BeginUpdate();
            arbol.Nodes.Clear();

            foreach (var permiso in raices.OrderBy(p => p is CompositePermission ? 0 : 1).ThenBy(p => p.Name))
            {
                arbol.Nodes.Add(CrearNodo(permiso));
            }

            arbol.ExpandAll();
            arbol.EndUpdate();
        }

        #endregion

        #region PERMISOS

        private void CargarArbolPermisos()
        {
            LlenarArbol(tvPermisos, _permisos.ObtenerRaices());

            cmbPermisoHijo.DataSource = null;
            cmbPermisoHijo.DataSource = _permisos.ListarTodo().OrderBy(p => p.Name).ToList();
            cmbPermisoHijo.DisplayMember = "Name";
        }

        private void btnCrearPermiso_Click(object sender, EventArgs e)
        {
            try
            {
                IPermission nuevo = rbCompuesto.Checked
                    ? _permisos.CrearPermisoCompuesto(txtNombrePermiso.Text.Trim(), txtDescripcionPermiso.Text.Trim())
                    : _permisos.CrearPermisoSimple(txtNombrePermiso.Text.Trim(), txtDescripcionPermiso.Text.Trim());

                if (_permisos.ListarTodo().Any(p => string.Equals(p.Name, nuevo.Name, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException(Traductor.T("FrmPermisos.MsgPermisoExiste", "Ya existe un permiso con ese nombre."));

                if (_permisos.Guardar(nuevo) < 0)
                    throw new Exception(Traductor.T("FrmPermisos.MsgNoGuardado", "No se pudo guardar en la base de datos."));

                // si había un compuesto seleccionado, el nuevo permiso se agrega como hijo
                if (tvPermisos.SelectedNode?.Tag is CompositePermission padre && chkAgregarAlSeleccionado.Checked)
                    _permisos.AsignarHijo(padre.Id, nuevo.Id);

                txtNombrePermiso.Clear();
                txtDescripcionPermiso.Clear();
                CargarArbolPermisos();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnAgregarHijo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(tvPermisos.SelectedNode?.Tag is CompositePermission padre))
                    throw new InvalidOperationException(Traductor.T("FrmPermisos.MsgSeleccioneCompuesto", "Seleccione en el árbol un permiso compuesto (en azul)."));

                if (!(cmbPermisoHijo.SelectedItem is IPermission hijo))
                    throw new InvalidOperationException(Traductor.T("FrmPermisos.MsgSeleccioneHijo", "Seleccione el permiso a agregar."));

                _permisos.AsignarHijo(padre.Id, hijo.Id);
                CargarArbolPermisos();
                RefrescarRolYUsuario();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnQuitarHijo_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode nodo = tvPermisos.SelectedNode;

                if (nodo?.Parent == null || !(nodo.Parent.Tag is CompositePermission padre))
                    throw new InvalidOperationException(Traductor.T("FrmPermisos.MsgSeleccioneNodoHijo", "Seleccione un permiso que esté DENTRO de un grupo."));

                _permisos.QuitarHijo(padre.Id, ((IPermission)nodo.Tag).Id);
                CargarArbolPermisos();
                RefrescarRolYUsuario();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnEliminarPermiso_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(tvPermisos.SelectedNode?.Tag is IPermission permiso))
                    throw new InvalidOperationException(Traductor.T("FrmPermisos.MsgSeleccionePermiso", "Seleccione un permiso del árbol."));

                if (MessageBox.Show(string.Format(Traductor.T("FrmPermisos.MsgConfirmarBorrado", "¿Eliminar el permiso '{0}'? Se quitará de todos los grupos y roles."), permiso.Name),
                        "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                _permisos.Borrar(permiso);
                CargarArbolPermisos();
                RefrescarRolYUsuario();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        #endregion

        #region ROLES

        private void CargarRoles()
        {
            var roles = _roles.ListarTodo();

            lstRoles.DataSource = null;
            lstRoles.DataSource = roles;
            lstRoles.DisplayMember = "Name";

            cmbRolUsuario.DataSource = null;
            cmbRolUsuario.DataSource = roles.ToList();
            cmbRolUsuario.DisplayMember = "Name";
        }

        private void lstRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MostrarPermisosDelRol();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void MostrarPermisosDelRol()
        {
            if (!(lstRoles.SelectedItem is Role seleccionado))
            {
                tvRol.Nodes.Clear();
                return;
            }

            Role rol = _roles.ObtenerPorId(seleccionado.Id);
            LlenarArbol(tvRol, rol?.Permisos ?? new List<IPermission>());
        }

        private void btnCrearRol_Click(object sender, EventArgs e)
        {
            try
            {
                Role rol = _roles.CrearRol(txtNombreRol.Text.Trim(), txtDescripcionRol.Text.Trim());

                if (_roles.ListarTodo().Any(r => string.Equals(r.Name, rol.Name, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException(Traductor.T("FrmPermisos.MsgRolExiste", "Ya existe un rol con ese nombre."));

                if (_roles.Guardar(rol) < 0)
                    throw new Exception(Traductor.T("FrmPermisos.MsgNoGuardado", "No se pudo guardar en la base de datos."));

                txtNombreRol.Clear();
                txtDescripcionRol.Clear();
                CargarRoles();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnEliminarRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(lstRoles.SelectedItem is Role rol))
                    return;

                if (MessageBox.Show(string.Format(Traductor.T("FrmPermisos.MsgConfirmarBorradoRol", "¿Eliminar el rol '{0}'? Se quitará a todos los usuarios."), rol.Name),
                        "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                _roles.Borrar(rol);
                CargarRoles();
                MostrarRolesDelUsuario();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnAsignarPermisoARol_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(lstRoles.SelectedItem is Role rol))
                    throw new InvalidOperationException(Traductor.T("FrmPermisos.MsgSeleccioneRol", "Seleccione un rol."));

                if (!(tvPermisos.SelectedNode?.Tag is IPermission permiso))
                    throw new InvalidOperationException(Traductor.T("FrmPermisos.MsgSeleccionePermiso", "Seleccione un permiso del árbol."));

                _roles.AsignarPermiso(rol.Id, permiso.Id);
                RefrescarRolYUsuario();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnQuitarPermisoDeRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(lstRoles.SelectedItem is Role rol))
                    return;

                TreeNode nodo = tvRol.SelectedNode;

                // sólo se pueden quitar los permisos asignados directamente (nodos raíz)
                while (nodo?.Parent != null)
                    nodo = nodo.Parent;

                if (!(nodo?.Tag is IPermission permiso))
                    throw new InvalidOperationException(Traductor.T("FrmPermisos.MsgSeleccionePermisoRol", "Seleccione un permiso del rol."));

                _roles.QuitarPermiso(rol.Id, permiso.Id);
                RefrescarRolYUsuario();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        #endregion

        #region USUARIOS

        private void CargarUsuarios()
        {
            cmbUsuarios.DataSource = null;
            cmbUsuarios.DataSource = _usuarios.GetAllUsers();
            cmbUsuarios.DisplayMember = "Username";
        }

        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MostrarRolesDelUsuario();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        /// <summary>Raíz = rol; debajo, el árbol de permisos de ese rol (recursivo).</summary>
        private void MostrarRolesDelUsuario()
        {
            tvUsuario.BeginUpdate();
            tvUsuario.Nodes.Clear();

            if (cmbUsuarios.SelectedItem is UserAccount usuario)
            {
                foreach (Role rol in _roles.ObtenerRolesPorUsuario(usuario.Id))
                {
                    var nodoRol = new TreeNode(rol.Name) { Tag = rol, ForeColor = Color.DarkGreen };
                    nodoRol.NodeFont = new Font(tvUsuario.Font, FontStyle.Bold);

                    foreach (IPermission permiso in rol.Permisos)
                        nodoRol.Nodes.Add(CrearNodo(permiso));

                    tvUsuario.Nodes.Add(nodoRol);
                }
            }

            tvUsuario.ExpandAll();
            tvUsuario.EndUpdate();
        }

        private void btnAsignarRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(cmbUsuarios.SelectedItem is UserAccount usuario) || !(cmbRolUsuario.SelectedItem is Role rol))
                    throw new InvalidOperationException(Traductor.T("FrmPermisos.MsgSeleccioneUsuarioRol", "Seleccione un usuario y un rol."));

                _roles.AsignarRolAUsuario(usuario.Id, rol.Id);
                MostrarRolesDelUsuario();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnQuitarRol_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode nodo = tvUsuario.SelectedNode;
                while (nodo?.Parent != null)
                    nodo = nodo.Parent;

                if (!(cmbUsuarios.SelectedItem is UserAccount usuario) || !(nodo?.Tag is Role rol))
                    throw new InvalidOperationException(Traductor.T("FrmPermisos.MsgSeleccioneRolUsuario", "Seleccione el rol del usuario a quitar."));

                _roles.QuitarRolAUsuario(usuario.Id, rol.Id);
                MostrarRolesDelUsuario();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        #endregion

        #region FUNCIONES/METODOS

        private void RefrescarRolYUsuario()
        {
            MostrarPermisosDelRol();
            MostrarRolesDelUsuario();
        }

        private static void MostrarError(Exception ex)
        {
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
