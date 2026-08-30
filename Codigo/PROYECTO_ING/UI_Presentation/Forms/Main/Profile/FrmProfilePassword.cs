using BE_Entities.Users;
using BLL_Business.Users_Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SERVICES;
using SERVICES.UserSession;
using BLL_Business.ActivityLog_Services;
using BE_Entities.ActivityLog;

namespace UI_Presentation.Forms.Profile
{
    public partial class FrmProfilePassword : Form
    {
        public FrmProfilePassword()
        {
            InitializeComponent();
        }

        Hash_Service _hashService;
        UserAccount_Services _userService;
        ActivityLog_Services _activityServices = new ActivityLog_Services();
        ActivityLog actividad = new ActivityLog();

        #region LOAD/CARGA INICIAL
        private void FrmProfilePassword_Load(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;

            _userService = new UserAccount_Services();
            _hashService = new Hash_Service();
        }
        #endregion

        #region BOTONES
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                UserAccount user = UserSession.GetInstance().UserAccount;
                string newPass = txtNewPass1.Text;

                if (_userService.ChangeUserPassword(user, newPass)) 
                {
                    MessageBox.Show("Contraseña modificada correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    actividad.FechaLog = DateTime.Now;
                    actividad.TipoLog = ActivityType.ChangePassword;
                    actividad.UserAccount = user;
                    _activityServices.GuardarActividad(actividad);

                    this.Close();
                }
                else
                {
                    actividad.FechaLog = DateTime.Now;
                    actividad.TipoLog = ActivityType.ChangePasswordFailed;
                    actividad.UserAccount = user;
                    _activityServices.GuardarActividad(actividad);
                    MessageBox.Show("No se pudo realizar la modificación de la contraseña.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimpiarCampos();
                }
            }
            catch(Exception ex)
            {
                throw new Exception("UI_FrmProfilePassword / " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region METODOS/FUNCIONES
        
        public void ValidarCampos()
        {
            // Validar ACTUAL
            string actualPass = _hashService.EncriptarPassword(txtActualPass.Text);
            string actual = UserSession.GetInstance().UserAccount.Password;
            
            bool actualOK = !string.IsNullOrWhiteSpace(txtActualPass.Text) && (actual == actualPass);
            ActualizarLabels(lblValidacionActualPass, actualOK, "Campo requerido");

            // Validar NUEVA
            string patron = @"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$";
            
            bool nuevaOK = Regex.IsMatch(txtNewPass1.Text, patron);
            ActualizarLabels(lblValidacionNewPass1, nuevaOK, "Falta mayúscula, número o símbolo (mín. 8)");

            // Validar COINCIDENCIA
            bool nuevasCoinciden = txtNewPass1.Text == txtNewPass2.Text && !string.IsNullOrWhiteSpace(txtNewPass2.Text);
            ActualizarLabels(lblValidacionNewPass2, nuevasCoinciden, "Las contraseñas no coinciden");


            btnGuardar.Enabled = actualOK && nuevaOK && nuevasCoinciden;
        }
        private void ActualizarLabels(Label label, bool esValido, string mensajeError)
        {
            if (esValido)
            {
                label.Text = "✔ Correcto";
                label.ForeColor = Color.FromArgb(132, 169, 140);
            }
            else
            {
                label.Text = "✖ " + mensajeError;
                label.ForeColor = Color.FromArgb(230, 57, 70);
            }
        }
        public void LimpiarCampos()
        {
            txtActualPass.Text = "";
            txtNewPass1.Text = "";
            txtNewPass2.Text = "";
        }
        #endregion

        #region EVENTOS
        private void txtActualPass_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }
        private void txtNewPass1_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        private void txtNewPass2_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }
        #endregion


    }
}
