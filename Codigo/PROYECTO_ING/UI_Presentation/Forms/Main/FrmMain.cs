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
using UI_Presentation.Forms.Admin;
using UI_Presentation.Forms.Profile;
using SERVICES.UserSession;
using UI_Presentation.Forms.Main.ActivityLog;

namespace UI_Presentation.Forms.Main
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        UserAccount user_logged = UserSession.GetInstance().UserAccount;

        #region LOAD/CARGA INICIAL
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = "PRINCIPAL";
            toolStripStatusLblUser.Text = $"{user_logged.Username}";
            this.WindowState = FormWindowState.Maximized;
            this.MaximumSize = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
        }
        #endregion

        #region MENU

            #region 1. ARCHIVO
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
