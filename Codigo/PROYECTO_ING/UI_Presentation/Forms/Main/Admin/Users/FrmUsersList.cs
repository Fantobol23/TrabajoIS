using BE_Entities.Users;
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

namespace UI_Presentation.Forms.Admin
{
    public partial class FrmUsersList : Form
    {
        public FrmUsersList()
        {
            InitializeComponent();
        }

        UserAccount_Services _userServices = new UserAccount_Services();

        #region LOAD/CARGA INICIAL
        private void FrmUsersList_Load(object sender, EventArgs e)
        {
            ActualizarGrilla();
            AplicarConfiguracionUI();
        }
        #endregion

        #region FUNCIONES/METODOS

        public void ActualizarGrilla()
        {
            dgvUsersAccounts.DataSource = null;
            dgvUsersAccounts.DataSource = _userServices.GetAllUsers();
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

            this.dgvUsersAccounts.Columns["Estado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvUsersAccounts.Columns["Estado"].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvUsersAccounts.Columns["Estado"].HeaderText = "ESTADO";
        }
        #endregion

        
    }
}
