using BE_Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_Presentation.Forms.F_Admin;
using UI_Presentation.Forms.F_Profile;
using SERVICES.UserSession;
using UI_Presentation.Forms.F_Main.F_ActivityLog;
using BLL_Business.ActivityLog_Services;
using BE_Entities.ActivityLog;
using BE_Entities.Integrity;
using BE_Entities.Languages;
using BLL_Business.Integrity_Services;
using BLL_Business.Language_Services;
using SERVICES.Languages;
using UI_Presentation.Forms.F_Security;
using UI_Presentation.Languages;

namespace UI_Presentation.Forms.F_Main
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            CrearMenusSeguridadEIdioma();

            this.Text = "PRINCIPAL";
            toolStripStatusLblUser.Tag = TraductorFormulario.TAG_NO_TRADUCIR; // muestra un dato, no una leyenda
            _traductor = TraductorFormulario.Registrar(this);
        }

        private readonly TraductorFormulario _traductor;
        private readonly Idioma_Services _idiomaServices = new Idioma_Services();

        // menús agregados por código (Administración > Seguridad, e Idioma)
        private ToolStripMenuItem seguridadToolStripMenuItem;
        private ToolStripMenuItem permisosToolStripMenuItem;
        private ToolStripMenuItem idiomasToolStripMenuItem;
        private ToolStripMenuItem integridadToolStripMenuItem;
        private ToolStripMenuItem idiomaToolStripMenuItem;

        ActivityLog_Services _activityServices = new ActivityLog_Services();
        ActivityLog actividad;
        UserAccount user_logged = UserSession.GetInstance().UserAccount;

        #region LOAD/CARGA INICIAL
        private void FrmMain_Load(object sender, EventArgs e)
        {
            toolStripStatusLblUser.Text = $"{user_logged.Username}";
            this.WindowState = FormWindowState.Maximized;
            this.MaximumSize = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;

            AplicarPermisos();
        }
        #endregion

        #region MENU

            #region 1. ARCHIVO
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actividad = new ActivityLog();
            actividad.FechaLog = DateTime.Now;
            actividad.TipoLog = ActivityType.Logout;
            actividad.UserAccount = UserSession.GetInstance().UserAccount;

            _activityServices.GuardarActividad(actividad);

            Application.Exit();
        }
            #endregion

            #region 2. PERFIL
        private void verInformaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProfileInfo frmProfile = new FrmProfileInfo();
            frmProfile.ShowDialog();
        }
        private void modificarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProfileEdit frmProfileEdit = new FrmProfileEdit();
            frmProfileEdit.ShowDialog();
        }
        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProfilePassword frmProfilePass = new FrmProfilePassword();
            frmProfilePass.ShowDialog();
        }
        #endregion

            #region 3. ADMINISTRACIÓN

        #region 3.1 USUARIOS
        private void altaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmUsersCreate frmUsersCreate = new FrmUsersCreate();
                frmUsersCreate.MdiParent = this;
                frmUsersCreate.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cambioestadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmUsersState frmUsersDelete = new FrmUsersState();
                frmUsersDelete.MdiParent = this;
                frmUsersDelete.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void modificacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmUsersEdit frmUsersEdit = new FrmUsersEdit();
                frmUsersEdit.MdiParent = this;
                frmUsersEdit.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void verListaCompletaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmUsersList frmUsersList = new FrmUsersList();
                frmUsersList.MdiParent = this;
                frmUsersList.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #endregion

        #region 3.2 SEGURIDAD
        private void permisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmPermisos())
                frm.ShowDialog(this);

            // los permisos del usuario pudieron cambiar: se recargan
            try
            {
                user_logged.Roles = new BLL_Business.Role_Services.Role_Services().ObtenerRolesPorUsuario(user_logged.Id);
                AplicarPermisos();
            }
            catch { }
        }
        private void idiomasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmIdiomas())
                frm.ShowDialog(this);
        }
        private void integridadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ResultadoIntegridad resultado = new DigitoVerificador_Services().VerificarIntegridad();

                if (resultado.EsValida)
                {
                    MessageBox.Show(Traductor.T("FrmMain.MsgIntegridadOk", "La base de datos está íntegra."), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var frm = new FrmIntegridad(resultado))
                    frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

            #region 4. BITACORA
        private void verActividadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmActivityLogList frmActivity = new FrmActivityLogList();
                frmActivity.MdiParent = this;
                frmActivity.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
            #endregion

        #endregion

        #region BOTONES/LINKS LABEL

        #endregion

        #region METODOS/FUNCIONES

        /// <summary>
        /// Habilita cada opción del menú según los permisos del usuario. La búsqueda en el
        /// árbol Composite es recursiva (Role.TienePermiso → IPermission.Contiene), así que
        /// tener un permiso compuesto otorga todos sus hijos.
        /// </summary>
        private void AplicarPermisos()
        {
            var permisosPorMenu = new Dictionary<ToolStripMenuItem, string>
            {
                { altaToolStripMenuItem, "USR_ALTA" },
                { cambioestadoToolStripMenuItem, "USR_ESTADO" },
                { modificacionToolStripMenuItem, "USR_MODIFICAR" },
                { verListaCompletaToolStripMenuItem, "USR_LISTAR" },
                { verActividadesToolStripMenuItem, "BIT_VER" },
                { permisosToolStripMenuItem, "SEG_PERMISOS_GESTIONAR" },
                { idiomasToolStripMenuItem, "SEG_IDIOMAS_GESTIONAR" },
                { integridadToolStripMenuItem, FrmIntegridad.PERMISO_RESTAURAR }
            };

            foreach (var par in permisosPorMenu)
            {
                par.Key.Available = user_logged.TienePermiso(par.Value);
            }

            OcultarMenusVacios(menuMain.Items);
        }

        /// <summary>Recursivo: un menú que sólo tiene submenús ocultos también se oculta.</summary>
        private static bool OcultarMenusVacios(ToolStripItemCollection items)
        {
            bool algunoVisible = false;

            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem menu && menu.DropDownItems.Count > 0)
                {
                    bool hijosVisibles = OcultarMenusVacios(menu.DropDownItems);
                    menu.Available = hijosVisibles || (menu.Tag as string) == "SiempreVisible";
                }

                // Available = estado propio del ítem (Visible depende de que el menú esté desplegado)
                if (item.Available)
                    algunoVisible = true;
            }

            return algunoVisible;
        }

        private void CrearMenusSeguridadEIdioma()
        {
            permisosToolStripMenuItem = new ToolStripMenuItem("Permisos y roles") { Name = "permisosToolStripMenuItem" };
            permisosToolStripMenuItem.Click += permisosToolStripMenuItem_Click;

            idiomasToolStripMenuItem = new ToolStripMenuItem("Idiomas y leyendas") { Name = "idiomasToolStripMenuItem" };
            idiomasToolStripMenuItem.Click += idiomasToolStripMenuItem_Click;

            integridadToolStripMenuItem = new ToolStripMenuItem("Verificar integridad de la BD") { Name = "integridadToolStripMenuItem" };
            integridadToolStripMenuItem.Click += integridadToolStripMenuItem_Click;

            seguridadToolStripMenuItem = new ToolStripMenuItem("Seguridad") { Name = "seguridadToolStripMenuItem" };
            seguridadToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { permisosToolStripMenuItem, idiomasToolStripMenuItem, integridadToolStripMenuItem });
            administraciónToolStripMenuItem.DropDownItems.Add(seguridadToolStripMenuItem);

            // menú Idioma: sus ítems (los idiomas) se arman al abrirlo, así aparecen los idiomas nuevos
            idiomaToolStripMenuItem = new ToolStripMenuItem("Idioma") { Name = "idiomaToolStripMenuItem", Tag = "SiempreVisible" };
            idiomaToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem("...") { Tag = TraductorFormulario.TAG_NO_TRADUCIR });
            idiomaToolStripMenuItem.DropDownOpening += idiomaToolStripMenuItem_DropDownOpening;
            menuMain.Items.Add(idiomaToolStripMenuItem);
        }

        private void idiomaToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            idiomaToolStripMenuItem.DropDownItems.Clear();

            try
            {
                Idioma actual = GestorIdioma.Instancia.IdiomaActual;

                foreach (Idioma idioma in _idiomaServices.ListarIdiomas())
                {
                    var item = new ToolStripMenuItem(idioma.Nombre)
                    {
                        Tag = TraductorFormulario.TAG_NO_TRADUCIR, // el nombre del idioma no se traduce
                        Checked = actual != null && actual.Id == idioma.Id
                    };
                    Idioma elegido = idioma;
                    item.Click += (s, ev) => CambiarIdioma(elegido);
                    idiomaToolStripMenuItem.DropDownItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                idiomaToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(ex.Message) { Enabled = false, Tag = TraductorFormulario.TAG_NO_TRADUCIR });
            }
        }

        private void CambiarIdioma(Idioma idioma)
        {
            try
            {
                // el GestorIdioma notifica a FrmMain y a todos los formularios abiertos
                _idiomaServices.CambiarIdioma(idioma);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region EVENTOS
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            UserSession.SessionLogout();
            Application.Exit();
        }
        #endregion

        
    }
}
