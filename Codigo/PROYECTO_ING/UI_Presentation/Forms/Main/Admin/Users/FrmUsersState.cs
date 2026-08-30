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
using System.CodeDom;
using BE_Entities.Users;
using BE_Entities.ActivityLog;
using SERVICES;
using BLL_Business.ActivityLog_Services;
using SERVICES.UserSession;

namespace UI_Presentation.Forms.Admin
{
    public partial class FrmUsersState : Form
    {
        public FrmUsersState()
        {
            InitializeComponent();
        }

        UserAccount_Services _userServices = new UserAccount_Services();
        UserStatus_Services _statusServices = new UserStatus_Services();
        ActivityLog_Services _activityServices = new ActivityLog_Services();

        UserStatus filtroStatus;
        ActivityLog actividad = new ActivityLog();

        #region LOAD/CARGA INICIAL
        
        private void FrmUsersState_Load(object sender, EventArgs e)
        {
            try
            {
                CargarComboEstados();
                cmbFilterStates.SelectedIndex = -1;
                dgvUsersAccounts.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region BOTONES
        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsersAccounts.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Error! No se ha seleccionado ningún usuario");
                }

                DialogResult confirm = MessageBox.Show("¿Confirma que desea habilitar el usuario seleccionado?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dgvUsersAccounts.SelectedRows.Count > 0 && confirm == DialogResult.Yes)
                {
                    DataGridViewRow fila = dgvUsersAccounts.SelectedRows[0];

                    UserAccount user_selec = (UserAccount)fila.DataBoundItem;

                    user_selec.Estado.Id = 1;
                    _userServices.EnableUser(user_selec);

                    filtroStatus = new UserStatus();
                    filtroStatus.Id = (int)cmbFilterStates.SelectedValue;
                    ActualizarGrilla(filtroStatus);
                    
                    AplicarConfiguracionUI();

                    actividad.FechaLog = DateTime.Now;
                    actividad.TipoLog = ActivityType.UserEnabled;
                    actividad.UserAccount = UserSession.GetInstance().UserAccount;

                    _activityServices.GuardarActividad(actividad);
                }
                if(confirm == DialogResult.No)
                {
                    MessageBox.Show("Operación anulada");
                }
            }
            catch
            {
                MessageBox.Show("Error al intentar habilitar la cuenta de usuario!");
            }
        }
        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsersAccounts.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Error! No se ha seleccionado ningún usuario");
                }

                DialogResult confirm = MessageBox.Show("¿Confirma que desea deshabilitar el usuario seleccionado?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dgvUsersAccounts.SelectedRows.Count > 0 && confirm == DialogResult.Yes)
                {
                    DataGridViewRow fila = dgvUsersAccounts.SelectedRows[0];

                    UserAccount user_selec = (UserAccount)fila.DataBoundItem;
                    user_selec.Estado.Id = 2;
                    _userServices.DisableUser(user_selec);

                    filtroStatus = new UserStatus();
                    filtroStatus.Id = (int)cmbFilterStates.SelectedValue;
                    ActualizarGrilla(filtroStatus);
                    AplicarConfiguracionUI();

                    actividad.FechaLog = DateTime.Now;
                    actividad.TipoLog = ActivityType.UserDisabled;
                    actividad.UserAccount = UserSession.GetInstance().UserAccount;

                    _activityServices.GuardarActividad(actividad);
                }
                if (confirm == DialogResult.No)
                {
                    MessageBox.Show("Operación anulada");
                }
            }
            catch
            {
                MessageBox.Show("Error al intentar deshabilitar la cuenta de usuario!");
            }
        }

        #endregion

        #region EVENTOS
        private void cmbFilterStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbFilterStates.SelectedIndex == -1 || cmbFilterStates.SelectedItem == null)
                {
                    return;
                }
                if (cmbFilterStates.SelectedItem is UserStatus selecStatus)
                {
                    filtroStatus = new UserStatus();
                    filtroStatus.Id = selecStatus.Id;
                    filtroStatus.StatusName = selecStatus.StatusName;

                    if(filtroStatus.StatusName == "Habilitado")
                    {
                        btnHabilitar.Enabled = false;
                        btnDeshabilitar.Enabled = true;
                    }
                    else
                    {
                        btnHabilitar.Enabled = true;
                        btnDeshabilitar.Enabled = false;
                    }

                    ActualizarGrilla(selecStatus);
                    AplicarConfiguracionUI();
                }
            }
            catch
            {
                MessageBox.Show("No se pudo actualizar el filtro seleccionado");
            }
        }
        
        #endregion

        #region FUNCIONES/MÉTODOS

        public void CargarComboEstados()
        {
            cmbFilterStates.DataSource = null;
            cmbFilterStates.DataSource = _statusServices.GetAllUsersStatus();

            cmbFilterStates.DisplayMember = "StatusName";
            cmbFilterStates.ValueMember = "Id";
        }
        public void ActualizarGrilla(UserStatus _status)
        {
            dgvUsersAccounts.DataSource = null;
            dgvUsersAccounts.DataSource = _userServices.GetUsersByState(_status);
        }
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
            this.dgvUsersAccounts.Columns["Estado"].Visible = false;

            this.dgvUsersAccounts.Columns["Username"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvUsersAccounts.Columns["Username"].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvUsersAccounts.Columns["Username"].HeaderText = "USERNAME";
            
            this.dgvUsersAccounts.Columns["Firstname"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvUsersAccounts.Columns["Firstname"].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvUsersAccounts.Columns["Firstname"].HeaderText = "NOMBRES";

            this.dgvUsersAccounts.Columns["Lastname"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvUsersAccounts.Columns["Lastname"].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvUsersAccounts.Columns["Lastname"].HeaderText = "APELLIDOS";

            this.dgvUsersAccounts.Columns["EmailOption1"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvUsersAccounts.Columns["EmailOption1"].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvUsersAccounts.Columns["EmailOption1"].HeaderText = "EMAIL";

            this.dgvUsersAccounts.Columns["EmailOption2"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvUsersAccounts.Columns["EmailOption2"].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvUsersAccounts.Columns["EmailOption2"].HeaderText = "EMAIL OPCIONAL";
        }

        #endregion
    }
}
