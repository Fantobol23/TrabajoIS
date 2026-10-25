using BE_Entities.ActivityLog;
using BLL_Business.ActivityLog_Services;
using SERVICES.UserSession;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_Presentation.Languages;

namespace UI_Presentation.Forms.F_Main.F_ActivityLog
{
    public partial class FrmActivityLogList : Form
    {
        public FrmActivityLogList()
        {
            InitializeComponent();
            TraductorFormulario.Registrar(this);
        }

        ActivityLog_Services _activityLogs = new ActivityLog_Services();
        ActivityLog actividad;

        // lista completa traída de la BD; el filtro por fecha trabaja sobre esta copia en memoria
        List<ActivityLog> _todasLasActividades = new List<ActivityLog>();
        private void FrmActivityLogList_Load(object sender, EventArgs e)
        {
            ActualizarGrilla();
            AplicarConfiguracionUI();

            actividad = new ActivityLog();
            actividad.FechaLog = DateTime.Now;
            actividad.TipoLog = ActivityType.ReadActivityLog;
            actividad.UserAccount = UserSession.GetInstance().UserAccount;

            _activityLogs.GuardarActividad(actividad);
        }

        #region FUNCIONES/METODOS

        public void ActualizarGrilla()
        {
            _todasLasActividades = _activityLogs.ObtenerTodos();

            dtpDesde.Value = _todasLasActividades.Count > 0 ? _todasLasActividades.Min(a => a.FechaLog).Date : DateTime.Today;
            dtpHasta.Value = DateTime.Today;

            MostrarActividades();
        }

        /// <summary>
        /// Si el check "Filtrar por fecha" está tildado muestra sólo los eventos entre
        /// Desde y Hasta (ambos días incluidos); si no, muestra todos.
        /// </summary>
        private void MostrarActividades()
        {
            IEnumerable<ActivityLog> resultado = _todasLasActividades;

            if (chkFiltrarFecha.Checked)
            {
                DateTime desde = dtpDesde.Value.Date;
                DateTime hasta = dtpHasta.Value.Date.AddDays(1); // hasta el final del día elegido

                resultado = resultado.Where(a => a.FechaLog >= desde && a.FechaLog < hasta);
            }

            dgvActivityLogs.DataSource = null;
            dgvActivityLogs.DataSource = resultado.ToList();

            if (dgvActivityLogs.Columns.Count > 0 && dgvActivityLogs.Columns.Contains("IdLog"))
                ConfigurarColumnas();
        }

        private void FiltroFecha_Changed(object sender, EventArgs e)
        {
            dtpDesde.Enabled = chkFiltrarFecha.Checked;
            dtpHasta.Enabled = chkFiltrarFecha.Checked;

            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show(Traductor.T("FrmActivityLogList.MsgRangoInvalido", "La fecha Desde no puede ser posterior a Hasta."), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDesde.Value = dtpHasta.Value.Date;
                return;
            }

            MostrarActividades();
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

            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
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
