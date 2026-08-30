namespace UI_Presentation.Forms.Main.ActivityLog
{
    partial class FrmActivityLogList
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
            this.buttonCloseForm1 = new UI_Presentation.Controls.ButtonCloseForm();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.dgvActivityLogs = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivityLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCloseForm1
            // 
            this.buttonCloseForm1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCloseForm1.BackColor = System.Drawing.Color.Transparent;
            this.buttonCloseForm1.Location = new System.Drawing.Point(788, 12);
            this.buttonCloseForm1.Name = "buttonCloseForm1";
            this.buttonCloseForm1.Size = new System.Drawing.Size(50, 48);
            this.buttonCloseForm1.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.lblTitulo.Location = new System.Drawing.Point(12, 27);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(301, 33);
            this.lblTitulo.TabIndex = 37;
            this.lblTitulo.Text = "LISTA DE ACTIVIDADES";
            // 
            // dgvActivityLogs
            // 
            this.dgvActivityLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvActivityLogs.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvActivityLogs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvActivityLogs.ColumnHeadersHeight = 29;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvActivityLogs.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvActivityLogs.EnableHeadersVisualStyles = false;
            this.dgvActivityLogs.Location = new System.Drawing.Point(99, 188);
            this.dgvActivityLogs.MultiSelect = false;
            this.dgvActivityLogs.Name = "dgvActivityLogs";
            this.dgvActivityLogs.RowHeadersWidth = 51;
            this.dgvActivityLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActivityLogs.Size = new System.Drawing.Size(650, 265);
            this.dgvActivityLogs.TabIndex = 39;
            // 
            // FrmActivityLogList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(53)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(850, 500);
            this.ControlBox = false;
            this.Controls.Add(this.dgvActivityLogs);
            this.Controls.Add(this.buttonCloseForm1);
            this.Controls.Add(this.lblTitulo);
            this.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmActivityLogList";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmActivityLogList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivityLogs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ButtonCloseForm buttonCloseForm1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvActivityLogs;
    }
}