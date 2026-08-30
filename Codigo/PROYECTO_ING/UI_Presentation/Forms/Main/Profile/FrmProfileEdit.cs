using BLL_Business.Users_Services;
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
using SERVICES;
using SERVICES.UserSession;
using BLL_Business.ActivityLog_Services;
using BE_Entities.ActivityLog;

namespace UI_Presentation.Forms.Profile
{
    public partial class FrmProfileEdit : Form
    {
        public FrmProfileEdit()
        {
            InitializeComponent();
        }

        UserAccount_Services _userService = new UserAccount_Services();
        ActivityLog_Services _activityServices = new ActivityLog_Services();
        ActivityLog actividad = new ActivityLog();

        #region LOAD/CARGA INICIAL
        private void FrmProfileEdit_Load(object sender, EventArgs e)
        {
            try
            {
                txtFirstname.Text = UserSession.GetInstance().UserAccount.FirstName.ToString();
                txtLastname.Text = UserSession.GetInstance().UserAccount.Lastname.ToString();
                txtEmailOpt1.Text = UserSession.GetInstance().UserAccount.EmailOption1.ToString();
                txtEmailOpt2.Text = UserSession.GetInstance().UserAccount.EmailOption2.ToString();
            }
            catch
            {
                MessageBox.Show("No se pudieron cargar los datos del perfil de usuario.");
            }
        }

        #endregion

        #region BOTONES
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtFirstname.Text) && 
                    !String.IsNullOrEmpty(txtLastname.Text) &&
                    !String.IsNullOrEmpty(txtEmailOpt1.Text) && 
                    !String.IsNullOrWhiteSpace(txtEmailOpt1.Text))
                {
                    UserAccount user = new UserAccount();
                    user.Username = UserSession.GetInstance().UserAccount.Username.ToString();
                    user.Password = UserSession.GetInstance().UserAccount.Password.ToString();
                    user.FirstName = txtFirstname.Text;
                    user.Lastname = txtLastname.Text;
                    user.EmailOption1 = txtEmailOpt1.Text;
                    user.EmailOption2 = txtEmailOpt2.Text;

                    _userService.UpdateUser(user);

                    actividad.FechaLog = DateTime.Now;
                    actividad.TipoLog = ActivityType.ChangeProfileInfo;
                    actividad.UserAccount = user;
                    _activityServices.GuardarActividad(actividad);

                    MessageBox.Show("Datos modificados correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    actividad.FechaLog = DateTime.Now;
                    actividad.TipoLog = ActivityType.ChangeProfileInfoFailed;
                    actividad.UserAccount = UserSession.GetInstance().UserAccount;
                    _activityServices.GuardarActividad(actividad);

                    MessageBox.Show("No se pudieron validar los campos, intente nuevamente.");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("UI_FrmProfileEdit / " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region METODOS/FUNCIONES

        #endregion

        #region EVENTOS

        #endregion
    }
}
