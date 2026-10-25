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
using UI_Presentation.Forms.F_Login_Register;
using UI_Presentation.Forms.F_Main;
using SERVICES;
using BLL_Business.Users_Services;
using BLL_Business.ActivityLog_Services;
using BE_Entities.ActivityLog;
using BE_Entities.Languages;
using BLL_Business.Language_Services;
using SERVICES.Languages;
using UI_Presentation.Languages;

namespace UI_Presentation
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            CrearSelectorIdioma();
            TraductorFormulario.Registrar(this);
        }

        private ComboBox cmbIdioma;
        private bool _cargandoIdiomas;

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

                        MessageBox.Show(Traductor.T("FrmLogin.MsgCredencialesIncorrectas", "Credenciales incorrectas"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LimpiarCampos();
                   }
                }
                else
                {
                    MessageBox.Show(Traductor.T("FrmLogin.MsgCompletarCampos", "Debe completar los campos."), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimpiarCampos();
                }
            }
            catch
            {
                MessageBox.Show(Traductor.T("FrmLogin.MsgNoIniciaSesion", "No fue posible iniciar la sesión."));
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

        /// <summary>Selector de idioma en el login: se puede elegir el idioma antes de ingresar.</summary>
        private void CrearSelectorIdioma()
        {
            cmbIdioma = new ComboBox
            {
                Name = "cmbIdioma",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(this.ClientSize.Width - 118, 8),
                Size = new Size(110, 24),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Tag = TraductorFormulario.TAG_NO_TRADUCIR
            };
            this.Controls.Add(cmbIdioma);
            cmbIdioma.BringToFront();

            try
            {
                _cargandoIdiomas = true;
                var idiomas = new Idioma_Services().ListarIdiomas();
                cmbIdioma.DataSource = idiomas;
                cmbIdioma.DisplayMember = "Nombre";

                Idioma actual = GestorIdioma.Instancia.IdiomaActual;
                if (actual != null)
                    cmbIdioma.SelectedItem = idiomas.FirstOrDefault(i => i.Id == actual.Id);
            }
            catch
            {
                cmbIdioma.Visible = false; // sin tablas de idiomas: sólo idioma por defecto
            }
            finally
            {
                _cargandoIdiomas = false;
            }

            cmbIdioma.SelectedIndexChanged += cmbIdioma_SelectedIndexChanged;
        }

        private void cmbIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoIdiomas || !(cmbIdioma.SelectedItem is Idioma idioma))
                return;

            try
            {
                new Idioma_Services().CambiarIdioma(idioma);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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
