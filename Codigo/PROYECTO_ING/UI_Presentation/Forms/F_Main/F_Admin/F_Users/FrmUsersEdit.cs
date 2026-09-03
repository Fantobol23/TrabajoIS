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
using BLL_Business.ActivityLog_Services;
using BE_Entities.ActivityLog;
using SERVICES.UserSession;

namespace UI_Presentation.Forms.F_Admin
{
    public partial class FrmUsersEdit : Form
    {
        public FrmUsersEdit()
        {
            InitializeComponent();
        }
        private static readonly Regex RegexPassword = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", RegexOptions.Compiled);
        private static readonly Regex RegexEmail = new Regex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", RegexOptions.Compiled);

        UserAccount_Services _userServices = new UserAccount_Services();
        ActivityLog_Services _activityServices = new ActivityLog_Services();
        UserStatus_Services _userStatusServices;
        Hash_Service _hashService;

        UserAccount user_seleccionado = new UserAccount();

        //Variables validaciones
        bool validacionCampos = false;
        bool validacionUsername = false;

        #region LOAD/CARGA INICIAL
        private void FrmUsersEdit_Load(object sender, EventArgs e)
        {
            //AplicarConfiguracionUI();
            try
            {
                CargarCombos();
                ActualizarGrilla();
                AplicarConfiguracionUI();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region BOTONES
        private void btnConfirmChanges_Click(object sender, EventArgs e)
        {
            ActivityLog actividad = new ActivityLog();

            try
            {
                if(validacionCampos && validacionUsername)
                {
                    _hashService = new Hash_Service();

                    user_seleccionado.Username = txtUsername.Text;
                    user_seleccionado.FirstName = txtFirstname.Text;
                    user_seleccionado.Lastname = txtLastname.Text;
                    user_seleccionado.EmailOption1 = txtEmailOpt1.Text;
                    user_seleccionado.EmailOption2 = txtEmailOpt2.Text;

                    _userServices.UpdateUser(user_seleccionado);

                    actividad.FechaLog = DateTime.Now;
                    actividad.TipoLog = ActivityType.UpdateUser;
                    actividad.UserAccount = UserSession.GetInstance().UserAccount;
                    _activityServices.GuardarActividad(actividad);
                }
                else
                {
                    MessageBox.Show("No se lograron realizar los cambios en el usuario. Intente nuevamente!");

                    actividad.FechaLog = DateTime.Now;
                    actividad.TipoLog = ActivityType.UpdateUserFailed;
                    actividad.UserAccount = UserSession.GetInstance().UserAccount;
                    _activityServices.GuardarActividad(actividad);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("UI_FrmUsersEdit / " + ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion

        #region FUNCIONES/MÉTODOS
        public void AplicarConfiguracionUI()
        {
            // FORMULARIO
            this.FormBorderStyle = FormBorderStyle.None;
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.Text = string.Empty;
            this.ControlBox = false;
            this.Dock = DockStyle.Fill;

            // DATAGRIDVIEW PERMISOS
            this.dgvUsersAccounts.AllowUserToAddRows = false;
            this.dgvUsersAccounts.AllowUserToDeleteRows = false;
            this.dgvUsersAccounts.AllowUserToResizeColumns = false;
            this.dgvUsersAccounts.AllowUserToResizeRows = false;
            this.dgvUsersAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.dgvUsersAccounts.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.dgvUsersAccounts.MultiSelect = false;
            this.dgvUsersAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsersAccounts.RowHeadersVisible = false;
            this.dgvUsersAccounts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvUsersAccounts.ColumnHeadersHeight = 40;
            this.dgvUsersAccounts.RowTemplate.Height = 30;

            // DATAGRIDVIEW DISEÑO
            Padding paddingColumnas = new Padding(0, 5, 0, 5);
            Padding paddingFilas = new Padding(0, 2, 0, 2);

            this.dgvUsersAccounts.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvUsersAccounts.DefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.dgvUsersAccounts.DefaultCellStyle.BackColor = Color.FromArgb(232, 237, 223);
            this.dgvUsersAccounts.DefaultCellStyle.ForeColor = Color.Black;
            this.dgvUsersAccounts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 203, 92);
            this.dgvUsersAccounts.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.dgvUsersAccounts.DefaultCellStyle.Padding = paddingFilas;

            this.dgvUsersAccounts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvUsersAccounts.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.dgvUsersAccounts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(207, 209, 213);
            this.dgvUsersAccounts.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            this.dgvUsersAccounts.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 203, 92);
            this.dgvUsersAccounts.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            this.dgvUsersAccounts.ColumnHeadersDefaultCellStyle.Padding = paddingColumnas;

            // DATAGRIDVIEW COLUMNAS Y FILAS
            this.dgvUsersAccounts.Columns["Id"].Visible = false;
            this.dgvUsersAccounts.Columns["Password"].Visible = false;
            this.dgvUsersAccounts.Columns["Firstname"].Visible = false;
            this.dgvUsersAccounts.Columns["Lastname"].Visible = false;
            this.dgvUsersAccounts.Columns["EmailOption1"].Visible = false;
            this.dgvUsersAccounts.Columns["EmailOption2"].Visible = false;
            this.dgvUsersAccounts.Columns["Estado"].Visible = false;

            this.dgvUsersAccounts.Columns["Username"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvUsersAccounts.Columns["Username"].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvUsersAccounts.Columns["Username"].HeaderText = "USERNAME";
        }
        public void ActualizarGrilla()
        {
            dgvUsersAccounts.DataSource = null;
            dgvUsersAccounts.DataSource = _userServices.GetAllUsers();
        }
        public void CargarCombos()
        {
            _userStatusServices = new UserStatus_Services();
            
            cmbStates.DataSource = null;
            cmbStates.DataSource = _userStatusServices.GetAllUsersStatus();

            cmbStates.DisplayMember = "StatusName";
            cmbStates.ValueMember = "Id";
        }
        public void ValidarUserDisponible()
        {
            try
            {
                string username_nuevo = txtUsername.Text;

                bool existe = _userServices.GetUserByUsername(username_nuevo);
                bool disponible;
                
                if (existe)
                {
                    disponible = false;
                    ActualizarLabel(lblValNewUsername, disponible, "Usuario ya existente");
                    validacionUsername = false; 
                }
                else
                {
                    disponible = true;
                    ActualizarLabel(lblValNewUsername, disponible, "");
                    validacionUsername = true;
                }
            }
            catch
            {
                MessageBox.Show("Error al validar la disponibilidad del username seleccionado");
            }
        }
        public void ValidarCampos()
        {
            string usernameText = txtUsername.Text;
            //string passwordText = txtPassword.Text;
            string firstnameText = txtFirstname.Text;
            string lastnameText = txtLastname.Text;
            string email1Text = txtEmailOpt1.Text;
            string email2Text = txtEmailOpt2.Text;

            // Username
            bool usernameOk = !string.IsNullOrWhiteSpace(usernameText);
            ActualizarLabel(lblValNewUsername, usernameOk, "Campo requerido");

            // Password
            //bool passTextOk = !string.IsNullOrWhiteSpace(passwordText);
            //bool passPatronOk = passTextOk && RegexPassword.IsMatch(passwordText);

            //if (!passTextOk)
            //    ActualizarLabel(lblValNewPass, false, "Campo requerido");
            //else
            //    ActualizarLabel(lblValNewPass, passPatronOk, "Contraseña no válida. Falta mayúscula, número o símbolo (mín. 8 caracteres)");

            // Nombre y Apellido
            bool firstnameOk = !string.IsNullOrWhiteSpace(firstnameText);
            ActualizarLabel(lblValNewFirstname, firstnameOk, "Campo requerido");

            bool lastnameOk = !string.IsNullOrWhiteSpace(lastnameText);
            ActualizarLabel(lblValNewLastname, lastnameOk, "Campo requerido");

            // Email 1 (Requerido)
            bool email1TextOk = !string.IsNullOrWhiteSpace(email1Text);
            bool email1PatronOk = email1TextOk && RegexEmail.IsMatch(email1Text);

            if (!email1TextOk)
                ActualizarLabel(lblValNewEmailOpt1, false, "Campo requerido");
            else
                ActualizarLabel(lblValNewEmailOpt1, email1PatronOk, "Email no válido");

            // Email 2 (Opcional)
            bool email2Empty = string.IsNullOrWhiteSpace(email2Text);
            bool email2PatronOk = email2Empty || RegexEmail.IsMatch(email2Text);

            if (email2Empty)
                ActualizarLabel(lblValNewEmailOpt2, true, "Campo opcional");
            else
                ActualizarLabel(lblValNewEmailOpt2, email2PatronOk, "Email no válido");

            if (usernameOk && firstnameOk && lastnameOk && email1PatronOk && email2PatronOk)
                validacionCampos = true;
            else
                validacionCampos = false;
        }
        private void ActualizarLabel(Label label, bool esValido, string mensajeError)
        {
            if (esValido)
            {
                label.Text = "✔";
                label.ForeColor = Color.FromArgb(132, 169, 140);
            }
            else
            {
                label.Text = "✖ " + mensajeError;
                label.ForeColor = Color.FromArgb(230, 57, 70);
            }
        }
        
        #endregion

        #region EVENTOS

        //Selección de la lista
        private void dgvUsersAccounts_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsersAccounts.SelectedRows.Count > 0)
                {
                    DataGridViewRow fila = dgvUsersAccounts.SelectedRows[0];

                    user_seleccionado = (UserAccount)fila.DataBoundItem;

                    txtUsername.Text = user_seleccionado.Username.ToString();
                    txtFirstname.Text = user_seleccionado.FirstName.ToString();
                    txtLastname.Text = user_seleccionado.Lastname.ToString();
                    txtEmailOpt1.Text = user_seleccionado.EmailOption1.ToString();
                    txtEmailOpt2.Text = user_seleccionado.EmailOption2.ToString();
                    cmbStates.SelectedValue = Convert.ToInt32(user_seleccionado.Estado.Id);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al seleccionar el elemento de la lista.");
            }
        }

        //Selección del combobox
        private void cmbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbStates.SelectedIndex == -1 || cmbStates.SelectedItem == null)
                {
                    return;
                }
                if (cmbStates.SelectedItem is UserStatus selecStatus)
                {
                    user_seleccionado.Estado.Id = selecStatus.Id;
                }
                    
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cambiar el estado del usuario seleccionado.");
            }
        }

        //Validaciones de los textbox
        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            ValidarUserDisponible();
        }
        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            ValidarCampos();
        }
        private void txtFirstname_Validating(object sender, CancelEventArgs e)
        {
            ValidarCampos();
        }
        private void txtLastname_Validating(object sender, CancelEventArgs e)
        {
            ValidarCampos();
        }
        private void txtEmailOpt1_Validating(object sender, CancelEventArgs e)
        {
            ValidarCampos();
        }
        private void txtEmailOpt2_Validating(object sender, CancelEventArgs e)
        {
            ValidarCampos();
        }

        #endregion


    }
}
