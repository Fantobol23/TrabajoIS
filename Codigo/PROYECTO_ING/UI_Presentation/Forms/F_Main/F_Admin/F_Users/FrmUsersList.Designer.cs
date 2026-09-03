namespace UI_Presentation.Forms.F_Admin
{
    partial class FrmUsersList
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
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.buttonCloseForm1 = new UI_Presentation.Controls.ButtonCloseForm();
            this.dgvUsersAccounts = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsersAccounts)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.lblFormTitle.Location = new System.Drawing.Point(12, 25);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(406, 33);
            this.lblFormTitle.TabIndex = 36;
            this.lblFormTitle.Text = "LISTA COMPLETA DE USUARIOS";
            // 
            // buttonCloseForm1
            // 
            this.buttonCloseForm1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCloseForm1.BackColor = System.Drawing.Color.Transparent;
            this.buttonCloseForm1.Location = new System.Drawing.Point(788, 13);
            this.buttonCloseForm1.Name = "buttonCloseForm1";
            this.buttonCloseForm1.Size = new System.Drawing.Size(50, 48);
            this.buttonCloseForm1.TabIndex = 37;
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
            this.dgvUsersAccounts.Location = new System.Drawing.Point(100, 166);
            this.dgvUsersAccounts.MultiSelect = false;
            this.dgvUsersAccounts.Name = "dgvUsersAccounts";
            this.dgvUsersAccounts.RowHeadersWidth = 51;
            this.dgvUsersAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsersAccounts.Size = new System.Drawing.Size(650, 265);
            this.dgvUsersAccounts.TabIndex = 38;
            // 
            // FrmUsersList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(53)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(850, 500);
            this.ControlBox = false;
            this.Controls.Add(this.dgvUsersAccounts);
            this.Controls.Add(this.buttonCloseForm1);
            this.Controls.Add(this.lblFormTitle);
            this.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUsersList";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmUsersList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsersAccounts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ButtonCloseForm buttonCloseForm1;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.DataGridView dgvUsersAccounts;
    }
}