namespace UI_Presentation.Forms.F_Security
{
    partial class FrmIdiomas
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
            this.grpIdiomas = new System.Windows.Forms.GroupBox();
            this.lstIdiomas = new System.Windows.Forms.ListBox();
            this.btnUsarIdioma = new System.Windows.Forms.Button();
            this.btnEliminarIdioma = new System.Windows.Forms.Button();
            this.lblNuevoIdioma = new System.Windows.Forms.Label();
            this.lblNombreIdioma = new System.Windows.Forms.Label();
            this.txtNombreIdioma = new System.Windows.Forms.TextBox();
            this.lblCodigoIdioma = new System.Windows.Forms.Label();
            this.txtCodigoIdioma = new System.Windows.Forms.TextBox();
            this.btnAgregarIdioma = new System.Windows.Forms.Button();
            this.grpTraducciones = new System.Windows.Forms.GroupBox();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.lblAyudaTraduccion = new System.Windows.Forms.Label();
            this.dgvTraducciones = new System.Windows.Forms.DataGridView();
            this.colClave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOriginal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTraduccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGuardarTraducciones = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraducciones)).BeginInit();
            this.grpIdiomas.SuspendLayout();
            this.grpTraducciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(15, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "IDIOMAS Y LEYENDAS";
            // 
            // grpIdiomas
            // 
            this.grpIdiomas.Controls.Add(this.lstIdiomas);
            this.grpIdiomas.Controls.Add(this.btnUsarIdioma);
            this.grpIdiomas.Controls.Add(this.btnEliminarIdioma);
            this.grpIdiomas.Controls.Add(this.lblNuevoIdioma);
            this.grpIdiomas.Controls.Add(this.lblNombreIdioma);
            this.grpIdiomas.Controls.Add(this.txtNombreIdioma);
            this.grpIdiomas.Controls.Add(this.lblCodigoIdioma);
            this.grpIdiomas.Controls.Add(this.txtCodigoIdioma);
            this.grpIdiomas.Controls.Add(this.btnAgregarIdioma);
            this.grpIdiomas.Location = new System.Drawing.Point(15, 50);
            this.grpIdiomas.Name = "grpIdiomas";
            this.grpIdiomas.Size = new System.Drawing.Size(300, 560);
            this.grpIdiomas.Text = "Idiomas";
            // 
            // lstIdiomas
            // 
            this.lstIdiomas.Location = new System.Drawing.Point(10, 25);
            this.lstIdiomas.Name = "lstIdiomas";
            this.lstIdiomas.Size = new System.Drawing.Size(280, 250);
            this.lstIdiomas.SelectedIndexChanged += new System.EventHandler(this.lstIdiomas_SelectedIndexChanged);
            // 
            // btnUsarIdioma
            // 
            this.btnUsarIdioma.UseVisualStyleBackColor = true;
            this.btnUsarIdioma.Location = new System.Drawing.Point(10, 282);
            this.btnUsarIdioma.Name = "btnUsarIdioma";
            this.btnUsarIdioma.Size = new System.Drawing.Size(280, 32);
            this.btnUsarIdioma.Text = "Usar este idioma";
            this.btnUsarIdioma.Click += new System.EventHandler(this.btnUsarIdioma_Click);
            // 
            // btnEliminarIdioma
            // 
            this.btnEliminarIdioma.UseVisualStyleBackColor = true;
            this.btnEliminarIdioma.Location = new System.Drawing.Point(10, 320);
            this.btnEliminarIdioma.Name = "btnEliminarIdioma";
            this.btnEliminarIdioma.Size = new System.Drawing.Size(280, 32);
            this.btnEliminarIdioma.Text = "Eliminar idioma";
            this.btnEliminarIdioma.Click += new System.EventHandler(this.btnEliminarIdioma_Click);
            // 
            // lblNuevoIdioma
            // 
            this.lblNuevoIdioma.AutoSize = true;
            this.lblNuevoIdioma.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblNuevoIdioma.Location = new System.Drawing.Point(10, 375);
            this.lblNuevoIdioma.Name = "lblNuevoIdioma";
            this.lblNuevoIdioma.Text = "Nuevo idioma";
            // 
            // lblNombreIdioma
            // 
            this.lblNombreIdioma.AutoSize = true;
            this.lblNombreIdioma.Location = new System.Drawing.Point(10, 405);
            this.lblNombreIdioma.Name = "lblNombreIdioma";
            this.lblNombreIdioma.Text = "Nombre";
            // 
            // txtNombreIdioma
            // 
            this.txtNombreIdioma.Location = new System.Drawing.Point(90, 402);
            this.txtNombreIdioma.Name = "txtNombreIdioma";
            this.txtNombreIdioma.Size = new System.Drawing.Size(200, 24);
            // 
            // lblCodigoIdioma
            // 
            this.lblCodigoIdioma.AutoSize = true;
            this.lblCodigoIdioma.Location = new System.Drawing.Point(10, 437);
            this.lblCodigoIdioma.Name = "lblCodigoIdioma";
            this.lblCodigoIdioma.Text = "Código";
            // 
            // txtCodigoIdioma
            // 
            this.txtCodigoIdioma.Location = new System.Drawing.Point(90, 434);
            this.txtCodigoIdioma.Name = "txtCodigoIdioma";
            this.txtCodigoIdioma.Size = new System.Drawing.Size(200, 24);
            // 
            // btnAgregarIdioma
            // 
            this.btnAgregarIdioma.UseVisualStyleBackColor = true;
            this.btnAgregarIdioma.Location = new System.Drawing.Point(10, 472);
            this.btnAgregarIdioma.Name = "btnAgregarIdioma";
            this.btnAgregarIdioma.Size = new System.Drawing.Size(280, 32);
            this.btnAgregarIdioma.Text = "Agregar idioma";
            this.btnAgregarIdioma.Click += new System.EventHandler(this.btnAgregarIdioma_Click);
            // 
            // grpTraducciones
            // 
            this.grpTraducciones.Controls.Add(this.lblFiltro);
            this.grpTraducciones.Controls.Add(this.txtFiltro);
            this.grpTraducciones.Controls.Add(this.lblAyudaTraduccion);
            this.grpTraducciones.Controls.Add(this.btnGuardarTraducciones);
            this.grpTraducciones.Location = new System.Drawing.Point(330, 50);
            this.grpTraducciones.Name = "grpTraducciones";
            this.grpTraducciones.Size = new System.Drawing.Size(740, 560);
            this.grpTraducciones.Text = "Leyendas del idioma seleccionado";
            // 
            // lblFiltro
            // 
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Location = new System.Drawing.Point(10, 28);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Text = "Buscar:";
            // 
            // txtFiltro
            // 
            this.txtFiltro.Location = new System.Drawing.Point(80, 25);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(300, 24);
            this.txtFiltro.TextChanged += new System.EventHandler(this.txtFiltro_TextChanged);
            // 
            // lblAyudaTraduccion
            // 
            this.lblAyudaTraduccion.AutoSize = true;
            this.lblAyudaTraduccion.ForeColor = System.Drawing.Color.DimGray;
            this.lblAyudaTraduccion.Location = new System.Drawing.Point(390, 28);
            this.lblAyudaTraduccion.Name = "lblAyudaTraduccion";
            this.lblAyudaTraduccion.Text = "Idioma por defecto: si la traducción queda vacía se usa el texto original.";
            // 
            // dgvTraducciones
            // 
            this.dgvTraducciones.AllowUserToAddRows = false;
            this.dgvTraducciones.AllowUserToDeleteRows = false;
            this.dgvTraducciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTraducciones.BackgroundColor = System.Drawing.Color.White;
            this.dgvTraducciones.RowHeadersVisible = false;
            this.dgvTraducciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colClave,
            this.colOriginal,
            this.colTraduccion});
            this.dgvTraducciones.Location = new System.Drawing.Point(10, 58);
            this.dgvTraducciones.Name = "dgvTraducciones";
            this.dgvTraducciones.Size = new System.Drawing.Size(720, 450);
            // 
            // colClave
            // 
            this.colClave.FillWeight = 90F;
            this.colClave.ReadOnly = true;
            this.colClave.HeaderText = "Clave";
            this.colClave.Name = "colClave";
            // 
            // colOriginal
            // 
            this.colOriginal.FillWeight = 100F;
            this.colOriginal.ReadOnly = true;
            this.colOriginal.HeaderText = "Texto original";
            this.colOriginal.Name = "colOriginal";
            // 
            // colTraduccion
            // 
            this.colTraduccion.FillWeight = 110F;
            this.colTraduccion.HeaderText = "Traducción";
            this.colTraduccion.Name = "colTraduccion";
            // 
            // btnGuardarTraducciones
            // 
            this.btnGuardarTraducciones.UseVisualStyleBackColor = true;
            this.btnGuardarTraducciones.Location = new System.Drawing.Point(530, 515);
            this.btnGuardarTraducciones.Name = "btnGuardarTraducciones";
            this.btnGuardarTraducciones.Size = new System.Drawing.Size(200, 34);
            this.btnGuardarTraducciones.Text = "Guardar traducciones";
            this.btnGuardarTraducciones.Click += new System.EventHandler(this.btnGuardarTraducciones_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Location = new System.Drawing.Point(950, 620);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 32);
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FrmIdiomas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(237)))), ((int)(((byte)(223)))));
            this.ClientSize = new System.Drawing.Size(1085, 665);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.grpIdiomas);
            this.Controls.Add(this.grpTraducciones);
            this.Controls.Add(this.btnCerrar);
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmIdiomas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IDIOMAS";
            this.Load += new System.EventHandler(this.FrmIdiomas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraducciones)).EndInit();
            this.grpIdiomas.ResumeLayout(false);
            this.grpIdiomas.PerformLayout();
            this.grpTraducciones.ResumeLayout(false);
            this.grpTraducciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpIdiomas;
        private System.Windows.Forms.ListBox lstIdiomas;
        private System.Windows.Forms.Button btnUsarIdioma;
        private System.Windows.Forms.Button btnEliminarIdioma;
        private System.Windows.Forms.Label lblNuevoIdioma;
        private System.Windows.Forms.Label lblNombreIdioma;
        private System.Windows.Forms.TextBox txtNombreIdioma;
        private System.Windows.Forms.Label lblCodigoIdioma;
        private System.Windows.Forms.TextBox txtCodigoIdioma;
        private System.Windows.Forms.Button btnAgregarIdioma;
        private System.Windows.Forms.GroupBox grpTraducciones;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.Label lblAyudaTraduccion;
        private System.Windows.Forms.DataGridView dgvTraducciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClave;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOriginal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTraduccion;
        private System.Windows.Forms.Button btnGuardarTraducciones;
        private System.Windows.Forms.Button btnCerrar;
    }
}
