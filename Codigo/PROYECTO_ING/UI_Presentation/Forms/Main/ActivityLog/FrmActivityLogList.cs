using BLL_Business.ActivityLog_Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_Presentation.Forms.Main.ActivityLog
{
    public partial class FrmActivityLogList : Form
    {
        public FrmActivityLogList()
        {
            InitializeComponent();
        }

        ActivityLog_Services _activityLogs = new ActivityLog_Services();

        private void FrmActivityLogList_Load(object sender, EventArgs e)
        {
            ActualizarGrilla();
            AplicarConfiguracionUI();
        }

        #region FUNCIONES/METODOS

        public void ActualizarGrilla()
        {
            dgvActivityLogs.DataSource = null;
            dgvActivityLogs.DataSource = _activityLogs.ObtenerTodos();
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
            this.dgvActivityLogs.AllowUserToAddRows = false;
            this.dgvActivityLogs.AllowUserToDeleteRows = false;
            this.dgvActivityLogs.AllowUserToResizeColumns = false;
            this.dgvActivityLogs.AllowUserToResizeRows = false;
            this.dgvActivityLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.dgvActivityLogs.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.dgvActivityLogs.MultiSelect = false;
            this.dgvActivityLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvActivityLogs.RowHeadersVisible = false;
            this.dgvActivityLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvActivityLogs.ColumnHeadersHeight = 40;
            this.dgvActivityLogs.RowTemplate.Height = 30;

            // DATAGRIDVIEW DISEÑO
            Padding paddingColumnas = new Padding(0, 5, 0, 5);
            Padding paddingFilas = new Padding(0, 2, 0, 2);

            this.dgvActivityLogs.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvActivityLogs.DefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.dgvActivityLogs.DefaultCellStyle.BackColor = Color.FromArgb(232, 237, 223);
            this.dgvActivityLogs.DefaultCellStyle.ForeColor = Color.Black;
            this.dgvActivityLogs.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 203, 92);
            this.dgvActivityLogs.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.dgvActivityLogs.DefaultCellStyle.Padding = paddingFilas;

            this.dgvActivityLogs.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvActivityLogs.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.dgvActivityLogs.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(207, 209, 213);
            this.dgvActivityLogs.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            this.dgvActivityLogs.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 203, 92);
            this.dgvActivityLogs.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            this.dgvActivityLogs.ColumnHeadersDefaultCellStyle.Padding = paddingColumnas;

            // DATAGRIDVIEW COLUMNAS Y FILAS
            
            this.dgvActivityLogs.Columns["IdLog"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvActivityLogs.Columns["IdLog"].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvActivityLogs.Columns["IdLog"].HeaderText = "ID LOG";

            this.dgvActivityLogs.Columns["FechaLog"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvActivityLogs.Columns["FechaLog"].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvActivityLogs.Columns["FechaLog"].HeaderText = "FECHA";

            this.dgvActivityLogs.Columns["TipoLog"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvActivityLogs.Columns["TipoLog"].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvActivityLogs.Columns["TipoLog"].HeaderText = "TIPO ACTIVIDAD";

            this.dgvActivityLogs.Columns["UserAccount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvActivityLogs.Columns["UserAccount"].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvActivityLogs.Columns["UserAccount"].HeaderText = "USUARIO";
        }
        #endregion
    }
}
