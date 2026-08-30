using System.Windows.Forms;

namespace UI_Presentation.Forms.Admin
{
    partial class FrmUsersState
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvUsersAccounts = new System.Windows.Forms.DataGridView();
            this.btnDeshabilitar = new System.Windows.Forms.Button();
            this.btnHabilitar = new System.Windows.Forms.Button();
            this.lblFiltros = new System.Windows.Forms.Label();
            this.cmbFilterStates = new System.Windows.Forms.ComboBox();
            this.btnCloseForm = new UI_Presentation.Controls.ButtonCloseForm();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsersAccounts)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.label2.Location = new System.Drawing.Point(12, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(239, 24);
            this.label2.TabIndex = 34;
            this.label2.Text = "ESTADOS DE USUARIOS";
            // 
            // dgvUsersAccounts
            // 
            this.dgvUsersAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUsersAccounts.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvUsersAccounts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvUsersAccounts.ColumnHeadersHeight = 29;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsersAccounts.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUsersAccounts.EnableHeadersVisualStyles = false;
            this.dgvUsersAccounts.Location = new System.Drawing.Point(100, 155);
            this.dgvUsersAccounts.MultiSelect = false;
            this.dgvUsersAccounts.Name = "dgvUsersAccounts";
            this.dgvUsersAccounts.RowHeadersWidth = 51;
            this.dgvUsersAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsersAccounts.Size = new System.Drawing.Size(650, 265);
            this.dgvUsersAccounts.TabIndex = 36;
            // 
            // btnDeshabilitar
            // 
            this.btnDeshabilitar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDeshabilitar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.btnDeshabilitar.FlatAppearance.BorderSize = 0;
            this.btnDeshabilitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeshabilitar.Image = global::UI_Presentation.Properties.Resources.dk_borrar_para_siempre_24;
            this.btnDeshabilitar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeshabilitar.Location = new System.Drawing.Point(530, 447);
            this.btnDeshabilitar.Name = "btnDeshabilitar";
            this.btnDeshabilitar.Size = new System.Drawing.Size(220, 50);
            this.btnDeshabilitar.TabIndex = 37;
            this.btnDeshabilitar.Text = "Deshabilitar";
            this.btnDeshabilitar.UseVisualStyleBackColor = true;
            this.btnDeshabilitar.Click += new System.EventHandler(this.btnDeshabilitar_Click);
            // 
            // btnHabilitar
            // 
            this.btnHabilitar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnHabilitar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.btnHabilitar.FlatAppearance.BorderSize = 0;
            this.btnHabilitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHabilitar.Image = global::UI_Presentation.Properties.Resources.dk_proceso_24;
            this.btnHabilitar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHabilitar.Location = new System.Drawing.Point(100, 447);
            this.btnHabilitar.Name = "btnHabilitar";
            this.btnHabilitar.Size = new System.Drawing.Size(220, 50);
            this.btnHabilitar.TabIndex = 38;
            this.btnHabilitar.Text = "Habilitar";
            this.btnHabilitar.UseVisualStyleBackColor = true;
            this.btnHabilitar.Click += new System.EventHandler(this.btnHabilitar_Click);
            // 
            // lblFiltros
            // 
            this.lblFiltros.AutoSize = true;
            this.lblFiltros.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.lblFiltros.Location = new System.Drawing.Point(106, 87);
            this.lblFiltros.Name = "lblFiltros";
            this.lblFiltros.Size = new System.Drawing.Size(98, 22);
            this.lblFiltros.TabIndex = 39;
            this.lblFiltros.Text = "Filtrar por:";
            // 
            // cmbFilterStates
            // 
            this.cmbFilterStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterStates.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterStates.FormattingEnabled = true;
            this.cmbFilterStates.Location = new System.Drawing.Point(231, 82);
            this.cmbFilterStates.Name = "cmbFilterStates";
            this.cmbFilterStates.Size = new System.Drawing.Size(262, 27);
            this.cmbFilterStates.TabIndex = 43;
            this.cmbFilterStates.SelectedIndexChanged += new System.EventHandler(this.cmbFilterStates_SelectedIndexChanged);
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseForm.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseForm.Location = new System.Drawing.Point(788, 13);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(50, 48);
            this.btnCloseForm.TabIndex = 35;
            // 
            // FrmUsersState
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(53)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(850, 530);
            this.ControlBox = false;
            this.Controls.Add(this.cmbFilterStates);
            this.Controls.Add(this.lblFiltros);
            this.Controls.Add(this.btnHabilitar);
            this.Controls.Add(this.btnDeshabilitar);
            this.Controls.Add(this.dgvUsersAccounts);
            this.Controls.Add(this.btnCloseForm);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(850, 530);
            this.Name = "FrmUsersState";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmUsersState_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsersAccounts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label2;
        private Controls.ButtonCloseForm btnCloseForm;
        private DataGridView dgvUsersAccounts;
        private Button btnDeshabilitar;
        private Button btnHabilitar;
        private Label lblFiltros;
        private ComboBox cmbFilterStates;
    }
}