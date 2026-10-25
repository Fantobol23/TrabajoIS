namespace UI_Presentation.Forms.F_Security
{
    partial class FrmPermisos
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
            this.grpPermisos = new System.Windows.Forms.GroupBox();
            this.tvPermisos = new System.Windows.Forms.TreeView();
            this.lblNombrePermiso = new System.Windows.Forms.Label();
            this.txtNombrePermiso = new System.Windows.Forms.TextBox();
            this.lblDescripcionPermiso = new System.Windows.Forms.Label();
            this.txtDescripcionPermiso = new System.Windows.Forms.TextBox();
            this.rbSimple = new System.Windows.Forms.RadioButton();
            this.rbCompuesto = new System.Windows.Forms.RadioButton();
            this.chkAgregarAlSeleccionado = new System.Windows.Forms.CheckBox();
            this.btnCrearPermiso = new System.Windows.Forms.Button();
            this.btnEliminarPermiso = new System.Windows.Forms.Button();
            this.lblPermisoHijo = new System.Windows.Forms.Label();
            this.cmbPermisoHijo = new System.Windows.Forms.ComboBox();
            this.btnAgregarHijo = new System.Windows.Forms.Button();
            this.btnQuitarHijo = new System.Windows.Forms.Button();
            this.grpRoles = new System.Windows.Forms.GroupBox();
            this.lstRoles = new System.Windows.Forms.ListBox();
            this.lblNombreRol = new System.Windows.Forms.Label();
            this.txtNombreRol = new System.Windows.Forms.TextBox();
            this.lblDescripcionRol = new System.Windows.Forms.Label();
            this.txtDescripcionRol = new System.Windows.Forms.TextBox();
            this.btnCrearRol = new System.Windows.Forms.Button();
            this.btnEliminarRol = new System.Windows.Forms.Button();
            this.lblPermisosRol = new System.Windows.Forms.Label();
            this.tvRol = new System.Windows.Forms.TreeView();
            this.btnAsignarPermisoARol = new System.Windows.Forms.Button();
            this.btnQuitarPermisoDeRol = new System.Windows.Forms.Button();
            this.grpUsuarios = new System.Windows.Forms.GroupBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.cmbUsuarios = new System.Windows.Forms.ComboBox();
            this.lblRolesUsuario = new System.Windows.Forms.Label();
            this.tvUsuario = new System.Windows.Forms.TreeView();
            this.lblRolAsignar = new System.Windows.Forms.Label();
            this.cmbRolUsuario = new System.Windows.Forms.ComboBox();
            this.btnAsignarRol = new System.Windows.Forms.Button();
            this.btnQuitarRol = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.grpPermisos.SuspendLayout();
            this.grpRoles.SuspendLayout();
            this.grpUsuarios.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(15, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "PERMISOS, ROLES Y USUARIOS (COMPOSITE)";
            // 
            // grpPermisos
            // 
            this.grpPermisos.Controls.Add(this.tvPermisos);
            this.grpPermisos.Controls.Add(this.lblNombrePermiso);
            this.grpPermisos.Controls.Add(this.txtNombrePermiso);
            this.grpPermisos.Controls.Add(this.lblDescripcionPermiso);
            this.grpPermisos.Controls.Add(this.txtDescripcionPermiso);
            this.grpPermisos.Controls.Add(this.rbSimple);
            this.grpPermisos.Controls.Add(this.rbCompuesto);
            this.grpPermisos.Controls.Add(this.chkAgregarAlSeleccionado);
            this.grpPermisos.Controls.Add(this.btnCrearPermiso);
            this.grpPermisos.Controls.Add(this.btnEliminarPermiso);
            this.grpPermisos.Controls.Add(this.lblPermisoHijo);
            this.grpPermisos.Controls.Add(this.cmbPermisoHijo);
            this.grpPermisos.Controls.Add(this.btnAgregarHijo);
            this.grpPermisos.Controls.Add(this.btnQuitarHijo);
            this.grpPermisos.Location = new System.Drawing.Point(15, 50);
            this.grpPermisos.Name = "grpPermisos";
            this.grpPermisos.Size = new System.Drawing.Size(420, 620);
            this.grpPermisos.Text = "Árbol de permisos";
            // 
            // tvPermisos
            // 
            this.tvPermisos.HideSelection = false;
            this.tvPermisos.ShowNodeToolTips = true;
            this.tvPermisos.Location = new System.Drawing.Point(10, 25);
            this.tvPermisos.Name = "tvPermisos";
            this.tvPermisos.Size = new System.Drawing.Size(400, 300);
            // 
            // lblNombrePermiso
            // 
            this.lblNombrePermiso.AutoSize = true;
            this.lblNombrePermiso.Location = new System.Drawing.Point(10, 335);
            this.lblNombrePermiso.Name = "lblNombrePermiso";
            this.lblNombrePermiso.Text = "Nombre";
            // 
            // txtNombrePermiso
            // 
            this.txtNombrePermiso.Location = new System.Drawing.Point(110, 332);
            this.txtNombrePermiso.Name = "txtNombrePermiso";
            this.txtNombrePermiso.Size = new System.Drawing.Size(300, 24);
            // 
            // lblDescripcionPermiso
            // 
            this.lblDescripcionPermiso.AutoSize = true;
            this.lblDescripcionPermiso.Location = new System.Drawing.Point(10, 365);
            this.lblDescripcionPermiso.Name = "lblDescripcionPermiso";
            this.lblDescripcionPermiso.Text = "Descripción";
            // 
            // txtDescripcionPermiso
            // 
            this.txtDescripcionPermiso.Location = new System.Drawing.Point(110, 362);
            this.txtDescripcionPermiso.Name = "txtDescripcionPermiso";
            this.txtDescripcionPermiso.Size = new System.Drawing.Size(300, 24);
            // 
            // rbSimple
            // 
            this.rbSimple.AutoSize = true;
            this.rbSimple.Location = new System.Drawing.Point(110, 392);
            this.rbSimple.Name = "rbSimple";
            this.rbSimple.Text = "Simple";
            // 
            // rbCompuesto
            // 
            this.rbCompuesto.AutoSize = true;
            this.rbCompuesto.Location = new System.Drawing.Point(200, 392);
            this.rbCompuesto.Name = "rbCompuesto";
            this.rbCompuesto.Text = "Compuesto (grupo)";
            // 
            // chkAgregarAlSeleccionado
            // 
            this.chkAgregarAlSeleccionado.AutoSize = true;
            this.chkAgregarAlSeleccionado.Location = new System.Drawing.Point(110, 418);
            this.chkAgregarAlSeleccionado.Name = "chkAgregarAlSeleccionado";
            this.chkAgregarAlSeleccionado.Text = "Agregar dentro del grupo seleccionado";
            // 
            // btnCrearPermiso
            // 
            this.btnCrearPermiso.UseVisualStyleBackColor = true;
            this.btnCrearPermiso.Location = new System.Drawing.Point(10, 445);
            this.btnCrearPermiso.Name = "btnCrearPermiso";
            this.btnCrearPermiso.Size = new System.Drawing.Size(195, 32);
            this.btnCrearPermiso.Text = "Crear permiso";
            this.btnCrearPermiso.Click += new System.EventHandler(this.btnCrearPermiso_Click);
            // 
            // btnEliminarPermiso
            // 
            this.btnEliminarPermiso.UseVisualStyleBackColor = true;
            this.btnEliminarPermiso.Location = new System.Drawing.Point(215, 445);
            this.btnEliminarPermiso.Name = "btnEliminarPermiso";
            this.btnEliminarPermiso.Size = new System.Drawing.Size(195, 32);
            this.btnEliminarPermiso.Text = "Eliminar seleccionado";
            this.btnEliminarPermiso.Click += new System.EventHandler(this.btnEliminarPermiso_Click);
            // 
            // lblPermisoHijo
            // 
            this.lblPermisoHijo.AutoSize = true;
            this.lblPermisoHijo.Location = new System.Drawing.Point(10, 495);
            this.lblPermisoHijo.Name = "lblPermisoHijo";
            this.lblPermisoHijo.Text = "Permiso a agregar al grupo seleccionado:";
            // 
            // cmbPermisoHijo
            // 
            this.cmbPermisoHijo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPermisoHijo.Location = new System.Drawing.Point(10, 518);
            this.cmbPermisoHijo.Name = "cmbPermisoHijo";
            this.cmbPermisoHijo.Size = new System.Drawing.Size(400, 24);
            // 
            // btnAgregarHijo
            // 
            this.btnAgregarHijo.UseVisualStyleBackColor = true;
            this.btnAgregarHijo.Location = new System.Drawing.Point(10, 552);
            this.btnAgregarHijo.Name = "btnAgregarHijo";
            this.btnAgregarHijo.Size = new System.Drawing.Size(195, 32);
            this.btnAgregarHijo.Text = "Agregar al grupo";
            this.btnAgregarHijo.Click += new System.EventHandler(this.btnAgregarHijo_Click);
            // 
            // btnQuitarHijo
            // 
            this.btnQuitarHijo.UseVisualStyleBackColor = true;
            this.btnQuitarHijo.Location = new System.Drawing.Point(215, 552);
            this.btnQuitarHijo.Name = "btnQuitarHijo";
            this.btnQuitarHijo.Size = new System.Drawing.Size(195, 32);
            this.btnQuitarHijo.Text = "Quitar del grupo";
            this.btnQuitarHijo.Click += new System.EventHandler(this.btnQuitarHijo_Click);
            // 
            // grpRoles
            // 
            this.grpRoles.Controls.Add(this.lstRoles);
            this.grpRoles.Controls.Add(this.lblNombreRol);
            this.grpRoles.Controls.Add(this.txtNombreRol);
            this.grpRoles.Controls.Add(this.lblDescripcionRol);
            this.grpRoles.Controls.Add(this.txtDescripcionRol);
            this.grpRoles.Controls.Add(this.btnCrearRol);
            this.grpRoles.Controls.Add(this.btnEliminarRol);
            this.grpRoles.Controls.Add(this.lblPermisosRol);
            this.grpRoles.Controls.Add(this.tvRol);
            this.grpRoles.Controls.Add(this.btnAsignarPermisoARol);
            this.grpRoles.Controls.Add(this.btnQuitarPermisoDeRol);
            this.grpRoles.Location = new System.Drawing.Point(450, 50);
            this.grpRoles.Name = "grpRoles";
            this.grpRoles.Size = new System.Drawing.Size(400, 620);
            this.grpRoles.Text = "Roles";
            // 
            // lstRoles
            // 
            this.lstRoles.Location = new System.Drawing.Point(10, 25);
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(380, 130);
            this.lstRoles.SelectedIndexChanged += new System.EventHandler(this.lstRoles_SelectedIndexChanged);
            // 
            // lblNombreRol
            // 
            this.lblNombreRol.AutoSize = true;
            this.lblNombreRol.Location = new System.Drawing.Point(10, 165);
            this.lblNombreRol.Name = "lblNombreRol";
            this.lblNombreRol.Text = "Nombre";
            // 
            // txtNombreRol
            // 
            this.txtNombreRol.Location = new System.Drawing.Point(110, 162);
            this.txtNombreRol.Name = "txtNombreRol";
            this.txtNombreRol.Size = new System.Drawing.Size(280, 24);
            // 
            // lblDescripcionRol
            // 
            this.lblDescripcionRol.AutoSize = true;
            this.lblDescripcionRol.Location = new System.Drawing.Point(10, 195);
            this.lblDescripcionRol.Name = "lblDescripcionRol";
            this.lblDescripcionRol.Text = "Descripción";
            // 
            // txtDescripcionRol
            // 
            this.txtDescripcionRol.Location = new System.Drawing.Point(110, 192);
            this.txtDescripcionRol.Name = "txtDescripcionRol";
            this.txtDescripcionRol.Size = new System.Drawing.Size(280, 24);
            // 
            // btnCrearRol
            // 
            this.btnCrearRol.UseVisualStyleBackColor = true;
            this.btnCrearRol.Location = new System.Drawing.Point(10, 225);
            this.btnCrearRol.Name = "btnCrearRol";
            this.btnCrearRol.Size = new System.Drawing.Size(185, 32);
            this.btnCrearRol.Text = "Crear rol";
            this.btnCrearRol.Click += new System.EventHandler(this.btnCrearRol_Click);
            // 
            // btnEliminarRol
            // 
            this.btnEliminarRol.UseVisualStyleBackColor = true;
            this.btnEliminarRol.Location = new System.Drawing.Point(205, 225);
            this.btnEliminarRol.Name = "btnEliminarRol";
            this.btnEliminarRol.Size = new System.Drawing.Size(185, 32);
            this.btnEliminarRol.Text = "Eliminar rol";
            this.btnEliminarRol.Click += new System.EventHandler(this.btnEliminarRol_Click);
            // 
            // lblPermisosRol
            // 
            this.lblPermisosRol.AutoSize = true;
            this.lblPermisosRol.Location = new System.Drawing.Point(10, 270);
            this.lblPermisosRol.Name = "lblPermisosRol";
            this.lblPermisosRol.Text = "Permisos del rol seleccionado:";
            // 
            // tvRol
            // 
            this.tvRol.HideSelection = false;
            this.tvRol.ShowNodeToolTips = true;
            this.tvRol.Location = new System.Drawing.Point(10, 292);
            this.tvRol.Name = "tvRol";
            this.tvRol.Size = new System.Drawing.Size(380, 250);
            // 
            // btnAsignarPermisoARol
            // 
            this.btnAsignarPermisoARol.UseVisualStyleBackColor = true;
            this.btnAsignarPermisoARol.Location = new System.Drawing.Point(10, 552);
            this.btnAsignarPermisoARol.Name = "btnAsignarPermisoARol";
            this.btnAsignarPermisoARol.Size = new System.Drawing.Size(185, 50);
            this.btnAsignarPermisoARol.Text = "Asignar permiso seleccionado en el árbol";
            this.btnAsignarPermisoARol.Click += new System.EventHandler(this.btnAsignarPermisoARol_Click);
            // 
            // btnQuitarPermisoDeRol
            // 
            this.btnQuitarPermisoDeRol.UseVisualStyleBackColor = true;
            this.btnQuitarPermisoDeRol.Location = new System.Drawing.Point(205, 552);
            this.btnQuitarPermisoDeRol.Name = "btnQuitarPermisoDeRol";
            this.btnQuitarPermisoDeRol.Size = new System.Drawing.Size(185, 50);
            this.btnQuitarPermisoDeRol.Text = "Quitar permiso del rol";
            this.btnQuitarPermisoDeRol.Click += new System.EventHandler(this.btnQuitarPermisoDeRol_Click);
            // 
            // grpUsuarios
            // 
            this.grpUsuarios.Controls.Add(this.lblUsuario);
            this.grpUsuarios.Controls.Add(this.cmbUsuarios);
            this.grpUsuarios.Controls.Add(this.lblRolesUsuario);
            this.grpUsuarios.Controls.Add(this.tvUsuario);
            this.grpUsuarios.Controls.Add(this.lblRolAsignar);
            this.grpUsuarios.Controls.Add(this.cmbRolUsuario);
            this.grpUsuarios.Controls.Add(this.btnAsignarRol);
            this.grpUsuarios.Controls.Add(this.btnQuitarRol);
            this.grpUsuarios.Location = new System.Drawing.Point(865, 50);
            this.grpUsuarios.Name = "grpUsuarios";
            this.grpUsuarios.Size = new System.Drawing.Size(400, 620);
            this.grpUsuarios.Text = "Usuarios";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(10, 28);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Text = "Usuario";
            // 
            // cmbUsuarios
            // 
            this.cmbUsuarios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsuarios.Location = new System.Drawing.Point(110, 25);
            this.cmbUsuarios.Name = "cmbUsuarios";
            this.cmbUsuarios.Size = new System.Drawing.Size(280, 24);
            this.cmbUsuarios.SelectedIndexChanged += new System.EventHandler(this.cmbUsuarios_SelectedIndexChanged);
            // 
            // lblRolesUsuario
            // 
            this.lblRolesUsuario.AutoSize = true;
            this.lblRolesUsuario.Location = new System.Drawing.Point(10, 60);
            this.lblRolesUsuario.Name = "lblRolesUsuario";
            this.lblRolesUsuario.Text = "Roles y permisos efectivos:";
            // 
            // tvUsuario
            // 
            this.tvUsuario.HideSelection = false;
            this.tvUsuario.ShowNodeToolTips = true;
            this.tvUsuario.Location = new System.Drawing.Point(10, 82);
            this.tvUsuario.Name = "tvUsuario";
            this.tvUsuario.Size = new System.Drawing.Size(380, 390);
            // 
            // lblRolAsignar
            // 
            this.lblRolAsignar.AutoSize = true;
            this.lblRolAsignar.Location = new System.Drawing.Point(10, 485);
            this.lblRolAsignar.Name = "lblRolAsignar";
            this.lblRolAsignar.Text = "Rol";
            // 
            // cmbRolUsuario
            // 
            this.cmbRolUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRolUsuario.Location = new System.Drawing.Point(110, 482);
            this.cmbRolUsuario.Name = "cmbRolUsuario";
            this.cmbRolUsuario.Size = new System.Drawing.Size(280, 24);
            // 
            // btnAsignarRol
            // 
            this.btnAsignarRol.UseVisualStyleBackColor = true;
            this.btnAsignarRol.Location = new System.Drawing.Point(10, 520);
            this.btnAsignarRol.Name = "btnAsignarRol";
            this.btnAsignarRol.Size = new System.Drawing.Size(185, 32);
            this.btnAsignarRol.Text = "Asignar rol";
            this.btnAsignarRol.Click += new System.EventHandler(this.btnAsignarRol_Click);
            // 
            // btnQuitarRol
            // 
            this.btnQuitarRol.UseVisualStyleBackColor = true;
            this.btnQuitarRol.Location = new System.Drawing.Point(205, 520);
            this.btnQuitarRol.Name = "btnQuitarRol";
            this.btnQuitarRol.Size = new System.Drawing.Size(185, 32);
            this.btnQuitarRol.Text = "Quitar rol seleccionado";
            this.btnQuitarRol.Click += new System.EventHandler(this.btnQuitarRol_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Location = new System.Drawing.Point(1145, 680);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 32);
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FrmPermisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(237)))), ((int)(((byte)(223)))));
            this.ClientSize = new System.Drawing.Size(1280, 725);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.grpPermisos);
            this.Controls.Add(this.grpRoles);
            this.Controls.Add(this.grpUsuarios);
            this.Controls.Add(this.btnCerrar);
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmPermisos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ADMINISTRACIÓN DE PERMISOS";
            this.Load += new System.EventHandler(this.FrmPermisos_Load);
            this.grpPermisos.ResumeLayout(false);
            this.grpPermisos.PerformLayout();
            this.grpRoles.ResumeLayout(false);
            this.grpRoles.PerformLayout();
            this.grpUsuarios.ResumeLayout(false);
            this.grpUsuarios.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpPermisos;
        private System.Windows.Forms.TreeView tvPermisos;
        private System.Windows.Forms.Label lblNombrePermiso;
        private System.Windows.Forms.TextBox txtNombrePermiso;
        private System.Windows.Forms.Label lblDescripcionPermiso;
        private System.Windows.Forms.TextBox txtDescripcionPermiso;
        private System.Windows.Forms.RadioButton rbSimple;
        private System.Windows.Forms.RadioButton rbCompuesto;
        private System.Windows.Forms.CheckBox chkAgregarAlSeleccionado;
        private System.Windows.Forms.Button btnCrearPermiso;
        private System.Windows.Forms.Button btnEliminarPermiso;
        private System.Windows.Forms.Label lblPermisoHijo;
        private System.Windows.Forms.ComboBox cmbPermisoHijo;
        private System.Windows.Forms.Button btnAgregarHijo;
        private System.Windows.Forms.Button btnQuitarHijo;
        private System.Windows.Forms.GroupBox grpRoles;
        private System.Windows.Forms.ListBox lstRoles;
        private System.Windows.Forms.Label lblNombreRol;
        private System.Windows.Forms.TextBox txtNombreRol;
        private System.Windows.Forms.Label lblDescripcionRol;
        private System.Windows.Forms.TextBox txtDescripcionRol;
        private System.Windows.Forms.Button btnCrearRol;
        private System.Windows.Forms.Button btnEliminarRol;
        private System.Windows.Forms.Label lblPermisosRol;
        private System.Windows.Forms.TreeView tvRol;
        private System.Windows.Forms.Button btnAsignarPermisoARol;
        private System.Windows.Forms.Button btnQuitarPermisoDeRol;
        private System.Windows.Forms.GroupBox grpUsuarios;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ComboBox cmbUsuarios;
        private System.Windows.Forms.Label lblRolesUsuario;
        private System.Windows.Forms.TreeView tvUsuario;
        private System.Windows.Forms.Label lblRolAsignar;
        private System.Windows.Forms.ComboBox cmbRolUsuario;
        private System.Windows.Forms.Button btnAsignarRol;
        private System.Windows.Forms.Button btnQuitarRol;
        private System.Windows.Forms.Button btnCerrar;
    }
}
