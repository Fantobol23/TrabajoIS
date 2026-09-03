using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_Business;
using BE_Entities;
using BLL_Business.Users_Services;
using BE_Entities.Users;
using SERVICES;
using BLL_Business.ActivityLog_Services;
using BE_Entities.ActivityLog;


namespace UI_Presentation.Forms.F_Login_Register
{
    public partial class FrmRegister : Form
    {
        public FrmRegister()
        {
            InitializeComponent();
        }

        UserAccount_Services _userService = new UserAccount_Services();
        ActivityLog_Services _activityService = new ActivityLog_Services();

        #region BOTONES
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            ActivityLog actividad = new ActivityLog();

            try
            {
                if (!String.IsNullOrEmpty(txtUsername.Text) && 
                    !String.IsNullOrEmpty(txtPassword.Text) && 
                    !String.IsNullOrEmpty(txtFirstName.Text) && 
                    !String.IsNullOrEmpty(txtLastname.Text) && 
                    !String.IsNullOrEmpty(txtEmailOpt1.Text))
                {
                    UserAccount userAccount = new UserAccount();
                    userAccount.Username = txtUsername.Text.ToLower();
                    userAccount.Password = txtPassword.Text;
                    userAccount.FirstName = txtFirstName.Text;
                    userAccount.Lastname = txtLastname.Text;
                    userAccount.EmailOption1 = txtEmailOpt1.Text;
                    userAccount.EmailOption2 = txtEmailOpt2.Text;

                    _userService.CreateUser(userAccount);
                    _userService = null;


                    actividad.TipoLog = ActivityType.CreateUser;
                    actividad.FechaLog = DateTime.Now;
                    actividad.UserAccount = userAccount;

                    _activityService.GuardarActividad(actividad);

                    MessageBox.Show("Usuario creado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                    FrmLogin frmLogin = new FrmLogin();
                    frmLogin.Show();
                }
                else
                {
                    MessageBox.Show("Debe completar los campos correctamente", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    LimpiarCampos();
                }
            }
            catch (Exception)
            {
                throw new Exception("No se puedo crear el usuario correctamente. Inténtelo nuevamente.");
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
        }

        #endregion

        #region MÉTODOS/EVENTOS

        public void LimpiarCampos()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtFirstName.Text = "";
            txtLastname.Text = "";
            txtEmailOpt1.Text = "";
            txtEmailOpt2.Text = "";
        }

        #endregion


    }
}
