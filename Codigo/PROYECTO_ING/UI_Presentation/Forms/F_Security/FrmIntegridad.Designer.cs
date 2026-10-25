namespace UI_Presentation.Forms.F_Security
{
    partial class FrmIntegridad
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.dgvErrores = new System.Windows.Forms.DataGridView();
            this.colTabla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpAdministrador = new System.Windows.Forms.GroupBox();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnRecalcular = new System.Windows.Forms.Button();
            this.btnReverificar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrores)).BeginInit();
            this.grpAdministrador.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTitulo.Location = new System.Drawing.Point(18, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "ERROR DE INTEGRIDAD EN LA BASE DE DATOS";
            // 
            // lblMensaje
            // 
            this.lblMensaje.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.lblMensaje.Location = new System.Drawing.Point(20, 50);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(840, 40);
            this.lblMensaje.Text = "Los dígitos verificadores detectaron datos modificados, agregados o eliminados por fuera del sistema. Informe al administrador. No se permite el ingreso hasta resolverlo.";
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblCantidad.Location = new System.Drawing.Point(20, 95);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Tag = "NoTraducir";
            this.lblCantidad.Text = "-";
            // 
            // dgvErrores
            // 
            this.dgvErrores.AllowUserToAddRows = false;
            this.dgvErrores.AllowUserToDeleteRows = false;
            this.dgvErrores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvErrores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvErrores.BackgroundColor = System.Drawing.Color.White;
            this.dgvErrores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTabla,
            this.colTipo,
            this.colRegistro,
            this.colDetalle});
            this.dgvErrores.Location = new System.Drawing.Point(20, 120);
            this.dgvErrores.Name = "dgvErrores";
            this.dgvErrores.ReadOnly = true;
            this.dgvErrores.RowHeadersVisible = false;
            this.dgvErrores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvErrores.Size = new System.Drawing.Size(840, 230);
            // 
            // colTabla
            // 
            this.colTabla.FillWeight = 60F;
            this.colTabla.HeaderText = "Tabla";
            this.colTabla.Name = "colTabla";
            this.colTabla.ReadOnly = true;
            // 
            // colTipo
            // 
            this.colTipo.FillWeight = 90F;
            this.colTipo.HeaderText = "Tipo de error";
            this.colTipo.Name = "colTipo";
            this.colTipo.ReadOnly = true;
            // 
            // colRegistro
            // 
            this.colRegistro.FillWeight = 70F;
            this.colRegistro.HeaderText = "Registro";
            this.colRegistro.Name = "colRegistro";
            this.colRegistro.ReadOnly = true;
            // 
            // colDetalle
            // 
            this.colDetalle.FillWeight = 160F;
            this.colDetalle.HeaderText = "Detalle";
            this.colDetalle.Name = "colDetalle";
            this.colDetalle.ReadOnly = true;
            // 
            // grpAdministrador
            // 
            this.grpAdministrador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAdministrador.Controls.Add(this.lblAyuda);
            this.grpAdministrador.Controls.Add(this.lblUsuario);
            this.grpAdministrador.Controls.Add(this.txtUsuario);
            this.grpAdministrador.Controls.Add(this.lblPassword);
            this.grpAdministrador.Controls.Add(this.txtPassword);
            this.grpAdministrador.Controls.Add(this.btnRecalcular);
            this.grpAdministrador.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.grpAdministrador.Location = new System.Drawing.Point(20, 360);
            this.grpAdministrador.Name = "grpAdministrador";
            this.grpAdministrador.Size = new System.Drawing.Size(840, 165);
            this.grpAdministrador.Text = "Restauración (sólo administrador)";
            // 
            // lblAyuda
            // 
            this.lblAyuda.Location = new System.Drawing.Point(15, 22);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(810, 60);
            this.lblAyuda.Text = "Opciones: 1) Restaurar el último backup de la base (SQL Server) y volver a verificar. 2) Corregir a mano los registros informados y volver a verificar. 3) Si los cambios son legítimos, recalcular los dígitos verificadores (el contenido actual queda como válido).";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(15, 95);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Text = "Usuario";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(15, 117);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(200, 24);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(230, 95);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Text = "Contraseña";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(230, 117);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(200, 24);
            // 
            // btnRecalcular
            // 
            this.btnRecalcular.Location = new System.Drawing.Point(450, 112);
            this.btnRecalcular.Name = "btnRecalcular";
            this.btnRecalcular.Size = new System.Drawing.Size(375, 34);
            this.btnRecalcular.Text = "Recalcular dígitos verificadores";
            this.btnRecalcular.UseVisualStyleBackColor = true;
            this.btnRecalcular.Click += new System.EventHandler(this.btnRecalcular_Click);
            // 
            // btnReverificar
            // 
            this.btnReverificar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReverificar.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnReverificar.Location = new System.Drawing.Point(560, 535);
            this.btnReverificar.Name = "btnReverificar";
            this.btnReverificar.Size = new System.Drawing.Size(170, 34);
            this.btnReverificar.Text = "Volver a verificar";
            this.btnReverificar.UseVisualStyleBackColor = true;
            this.btnReverificar.Click += new System.EventHandler(this.btnReverificar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnSalir.Location = new System.Drawing.Point(740, 535);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(120, 34);
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // FrmIntegridad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(237)))), ((int)(((byte)(223)))));
            this.ClientSize = new System.Drawing.Size(880, 585);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.dgvErrores);
            this.Controls.Add(this.grpAdministrador);
            this.Controls.Add(this.btnReverificar);
            this.Controls.Add(this.btnSalir);
            this.MinimumSize = new System.Drawing.Size(896, 624);
            this.Name = "FrmIntegridad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VERIFICACIÓN DE INTEGRIDAD";
            this.Load += new System.EventHandler(this.FrmIntegridad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrores)).EndInit();
            this.grpAdministrador.ResumeLayout(false);
            this.grpAdministrador.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.DataGridView dgvErrores;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTabla;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRegistro;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle;
        private System.Windows.Forms.GroupBox grpAdministrador;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnRecalcular;
        private System.Windows.Forms.Button btnReverificar;
        private System.Windows.Forms.Button btnSalir;
    }
}
