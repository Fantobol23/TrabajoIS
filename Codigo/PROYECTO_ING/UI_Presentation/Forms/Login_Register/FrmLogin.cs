using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_Entities;
using BE_Entities.Users;
using BLL_Business;
using UI_Presentation.Forms.Login_Register;
using UI_Presentation.Forms.Main;
using SERVICES;
using BLL_Business.Users_Services;
using BLL_Business.ActivityLog_Services;
using BE_Entities.ActivityLog;

namespace UI_Presentation
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        Hash_Service _hashService = new Hash_Service();
        UserAccount_Services _userServices = new UserAccount_Services();
        ActivityLog_Services _activityServices = new ActivityLog_Services();

        #region LOAD/CARGA INICIAL
        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
        
        #endregion

        #region BOTONES / LINKS LABELS

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                ActivityLog activity = new ActivityLog();
                activity.FechaLog = DateTime.Now;

                if (!String.IsNullOrEmpty(txtUsername.Text) && !String.IsNullOrEmpty(txtPassword.Text))
                {
                    UserAccount user = new UserAccount();
                    user.Username = txtUsername.Text;
                    user.Password = _hashService.EncriptarPassword(txtPassword.Text);

                   if (_userServices.LoginUser(user))
                   {
                        activity.TipoLog = ActivityType.Login;
                        activity.UserAccount = user;
                        _activityServices.GuardarActividad(activity);

                        FrmMain frmMain = new FrmMain();
                        frmMain.Show();
                        this.Hide();
                   }
                   else
                   {
                        activity.TipoLog = ActivityType.LoginFailed;
                        activity.UserAccount = user;
                        _activityServices.GuardarActividad(activity);

                        MessageBox.Show("Credenciales incorrectas","", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LimpiarCampos();
                   }
                }
                else
                {
                    MessageBox.Show("Debe completar los campos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimpiarCampos();
                }
            }
            catch
            {
                MessageBox.Show("No fue posible iniciar la sesión.");
                LimpiarCampos();
            }
        }
        private void llblCambiarPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        private void llblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmRegister frmRegister = new FrmRegister();
            frmRegister.Show();
            this.Hide();
        }
        
        #endregion

        #region FUNCIONES/METODOS

        public void LimpiarCampos()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        #endregion

        #region EVENTOS
        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}
