using System.Drawing;

namespace UI_Presentation
{
    partial class FrmLogin
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.llblCambiarPass = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pboxPassword = new System.Windows.Forms.PictureBox();
            this.pboxUsername = new System.Windows.Forms.PictureBox();
            this.llblRegister = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxUsername)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(78, 190);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(2);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(207, 27);
            this.txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(78, 244);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(207, 27);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(98, 303);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(165, 57);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Iniciar sesión";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // llblCambiarPass
            // 
            this.llblCambiarPass.ActiveLinkColor = System.Drawing.Color.White;
            this.llblCambiarPass.AutoSize = true;
            this.llblCambiarPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.llblCambiarPass.DisabledLinkColor = System.Drawing.Color.White;
            this.llblCambiarPass.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llblCambiarPass.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.llblCambiarPass.Location = new System.Drawing.Point(103, 378);
            this.llblCambiarPass.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llblCambiarPass.Name = "llblCambiarPass";
            this.llblCambiarPass.Size = new System.Drawing.Size(169, 17);
            this.llblCambiarPass.TabIndex = 4;
            this.llblCambiarPass.TabStop = true;
            this.llblCambiarPass.Text = "¿Olvidó su contraseña?";
            this.llblCambiarPass.VisitedLinkColor = System.Drawing.Color.White;
            this.llblCambiarPass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblCambiarPass_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::UI_Presentation.Properties.Resources.yw_user3_100;
            this.pictureBox1.Location = new System.Drawing.Point(124, 24);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(112, 122);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pboxPassword
            // 
            this.pboxPassword.Image = global::UI_Presentation.Properties.Resources.yw_key1_60;
            this.pboxPassword.Location = new System.Drawing.Point(42, 242);
            this.pboxPassword.Margin = new System.Windows.Forms.Padding(2);
            this.pboxPassword.Name = "pboxPassword";
            this.pboxPassword.Size = new System.Drawing.Size(27, 29);
            this.pboxPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxPassword.TabIndex = 2;
            this.pboxPassword.TabStop = false;
            // 
            // pboxUsername
            // 
            this.pboxUsername.Image = global::UI_Presentation.Properties.Resources.yw_user4_48;
            this.pboxUsername.Location = new System.Drawing.Point(42, 188);
            this.pboxUsername.Margin = new System.Windows.Forms.Padding(2);
            this.pboxUsername.Name = "pboxUsername";
            this.pboxUsername.Size = new System.Drawing.Size(27, 29);
            this.pboxUsername.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxUsername.TabIndex = 0;
            this.pboxUsername.TabStop = false;
            // 
            // llblRegister
            // 
            this.llblRegister.ActiveLinkColor = System.Drawing.Color.White;
            this.llblRegister.AutoSize = true;
            this.llblRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.llblRegister.DisabledLinkColor = System.Drawing.Color.White;
            this.llblRegister.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llblRegister.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.llblRegister.Location = new System.Drawing.Point(75, 408);
            this.llblRegister.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llblRegister.Name = "llblRegister";
            this.llblRegister.Size = new System.Drawing.Size(232, 17);
            this.llblRegister.TabIndex = 6;
            this.llblRegister.TabStop = true;
            this.llblRegister.Text = "¿No posee un usuario? Regístrese";
            this.llblRegister.VisitedLinkColor = System.Drawing.Color.White;
            this.llblRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblRegister_LinkClicked);
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(53)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(359, 446);
            this.Controls.Add(this.llblRegister);
            this.Controls.Add(this.llblCambiarPass);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.pboxPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.pboxUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LOGIN";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmLogin_FormClosed);
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxUsername)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pboxUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.PictureBox pboxPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel llblCambiarPass;
        private System.Windows.Forms.LinkLabel llblRegister;
    }
}

