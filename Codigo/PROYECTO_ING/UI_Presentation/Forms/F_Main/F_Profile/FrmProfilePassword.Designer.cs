namespace UI_Presentation.Forms.F_Profile
{
    partial class FrmProfilePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProfilePassword));
            this.txtNewPass2 = new System.Windows.Forms.TextBox();
            this.txtNewPass1 = new System.Windows.Forms.TextBox();
            this.txtActualPass = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblEmailOpt1 = new System.Windows.Forms.Label();
            this.lblLastname = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.pboxProfilePic = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblValidacionActualPass = new System.Windows.Forms.Label();
            this.lblValidacionNewPass1 = new System.Windows.Forms.Label();
            this.lblValidacionNewPass2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pboxProfilePic)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNewPass2
            // 
            this.txtNewPass2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPass2.Location = new System.Drawing.Point(398, 181);
            this.txtNewPass2.Name = "txtNewPass2";
            this.txtNewPass2.PasswordChar = '*';
            this.txtNewPass2.Size = new System.Drawing.Size(300, 27);
            this.txtNewPass2.TabIndex = 27;
            this.txtNewPass2.UseSystemPasswordChar = true;
            this.txtNewPass2.TextChanged += new System.EventHandler(this.txtNewPass2_TextChanged);
            // 
            // txtNewPass1
            // 
            this.txtNewPass1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPass1.Location = new System.Drawing.Point(398, 114);
            this.txtNewPass1.Name = "txtNewPass1";
            this.txtNewPass1.PasswordChar = '*';
            this.txtNewPass1.Size = new System.Drawing.Size(300, 27);
            this.txtNewPass1.TabIndex = 26;
            this.txtNewPass1.UseSystemPasswordChar = true;
            this.txtNewPass1.TextChanged += new System.EventHandler(this.txtNewPass1_TextChanged);
            // 
            // txtActualPass
            // 
            this.txtActualPass.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActualPass.Location = new System.Drawing.Point(398, 45);
            this.txtActualPass.Name = "txtActualPass";
            this.txtActualPass.PasswordChar = '*';
            this.txtActualPass.Size = new System.Drawing.Size(300, 27);
            this.txtActualPass.TabIndex = 25;
            this.txtActualPass.UseSystemPasswordChar = true;
            this.txtActualPass.TextChanged += new System.EventHandler(this.txtActualPass_TextChanged);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Image = global::UI_Presentation.Properties.Resources.dk_casilla_de_verificación_marcada_24;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(112, 283);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(220, 50);
            this.btnGuardar.TabIndex = 24;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = global::UI_Presentation.Properties.Resources.dk_cerrar_ventana_24;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(398, 283);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(220, 50);
            this.btnCancelar.TabIndex = 23;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblEmailOpt1
            // 
            this.lblEmailOpt1.AutoSize = true;
            this.lblEmailOpt1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailOpt1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.lblEmailOpt1.Location = new System.Drawing.Point(159, 186);
            this.lblEmailOpt1.Name = "lblEmailOpt1";
            this.lblEmailOpt1.Size = new System.Drawing.Size(226, 19);
            this.lblEmailOpt1.TabIndex = 21;
            this.lblEmailOpt1.Text = "Repita la nueva contraseña:";
            // 
            // lblLastname
            // 
            this.lblLastname.AutoSize = true;
            this.lblLastname.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.lblLastname.Location = new System.Drawing.Point(159, 119);
            this.lblLastname.Name = "lblLastname";
            this.lblLastname.Size = new System.Drawing.Size(233, 19);
            this.lblLastname.TabIndex = 20;
            this.lblLastname.Text = "Ingrese la nueva contraseña:";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.lblFirstName.Location = new System.Drawing.Point(159, 49);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(233, 19);
            this.lblFirstName.TabIndex = 19;
            this.lblFirstName.Text = "Ingrese la actual contraseña:";
            // 
            // pboxProfilePic
            // 
            this.pboxProfilePic.Image = global::UI_Presentation.Properties.Resources.yw_orientación_de_bloqueo_96;
            this.pboxProfilePic.Location = new System.Drawing.Point(37, 51);
            this.pboxProfilePic.Name = "pboxProfilePic";
            this.pboxProfilePic.Size = new System.Drawing.Size(100, 100);
            this.pboxProfilePic.TabIndex = 18;
            this.pboxProfilePic.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.label1.Location = new System.Drawing.Point(109, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(553, 16);
            this.label1.TabIndex = 28;
            this.label1.Text = "* Debe utilizar al menos una mayúscula, símbolos y número para mayor la seguridad" +
    "";
            // 
            // lblValidacionActualPass
            // 
            this.lblValidacionActualPass.AutoSize = true;
            this.lblValidacionActualPass.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValidacionActualPass.ForeColor = System.Drawing.SystemColors.Control;
            this.lblValidacionActualPass.Location = new System.Drawing.Point(404, 78);
            this.lblValidacionActualPass.Name = "lblValidacionActualPass";
            this.lblValidacionActualPass.Size = new System.Drawing.Size(75, 15);
            this.lblValidacionActualPass.TabIndex = 29;
            this.lblValidacionActualPass.Text = "* validación";
            // 
            // lblValidacionNewPass1
            // 
            this.lblValidacionNewPass1.AutoSize = true;
            this.lblValidacionNewPass1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValidacionNewPass1.ForeColor = System.Drawing.SystemColors.Control;
            this.lblValidacionNewPass1.Location = new System.Drawing.Point(404, 148);
            this.lblValidacionNewPass1.Name = "lblValidacionNewPass1";
            this.lblValidacionNewPass1.Size = new System.Drawing.Size(75, 15);
            this.lblValidacionNewPass1.TabIndex = 30;
            this.lblValidacionNewPass1.Text = "* validación";
            // 
            // lblValidacionNewPass2
            // 
            this.lblValidacionNewPass2.AutoSize = true;
            this.lblValidacionNewPass2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValidacionNewPass2.ForeColor = System.Drawing.SystemColors.Control;
            this.lblValidacionNewPass2.Location = new System.Drawing.Point(404, 215);
            this.lblValidacionNewPass2.Name = "lblValidacionNewPass2";
            this.lblValidacionNewPass2.Size = new System.Drawing.Size(75, 15);
            this.lblValidacionNewPass2.TabIndex = 31;
            this.lblValidacionNewPass2.Text = "* validación";
            // 
            // FrmProfilePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(53)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(730, 354);
            this.Controls.Add(this.lblValidacionNewPass2);
            this.Controls.Add(this.lblValidacionNewPass1);
            this.Controls.Add(this.lblValidacionActualPass);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNewPass2);
            this.Controls.Add(this.txtNewPass1);
            this.Controls.Add(this.txtActualPass);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lblEmailOpt1);
            this.Controls.Add(this.lblLastname);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.pboxProfilePic);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProfilePassword";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PERFIL - Cambiar contraseña";
            this.Load += new System.EventHandler(this.FrmProfilePassword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pboxProfilePic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtNewPass2;
        private System.Windows.Forms.TextBox txtNewPass1;
        private System.Windows.Forms.TextBox txtActualPass;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblEmailOpt1;
        private System.Windows.Forms.Label lblLastname;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.PictureBox pboxProfilePic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblValidacionActualPass;
        private System.Windows.Forms.Label lblValidacionNewPass1;
        private System.Windows.Forms.Label lblValidacionNewPass2;
    }
}