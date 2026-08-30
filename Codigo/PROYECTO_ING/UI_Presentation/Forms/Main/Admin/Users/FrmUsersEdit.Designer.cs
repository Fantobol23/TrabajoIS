using System.Windows.Forms;

namespace UI_Presentation.Forms.Admin
{
    partial class FrmUsersEdit
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
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.dgvUsersAccounts = new System.Windows.Forms.DataGridView();
            this.lblUsername = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFirstname = new System.Windows.Forms.Label();
            this.lblLastname = new System.Windows.Forms.Label();
            this.lblEmailOpt1 = new System.Windows.Forms.Label();
            this.lblEmailOpt2 = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.btnConfirmChanges = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtUserSearch = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblValNewEmailOpt2 = new System.Windows.Forms.Label();
            this.lblValNewUsername = new System.Windows.Forms.Label();
            this.lblValNewEmailOpt1 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblValNewLastname = new System.Windows.Forms.Label();
            this.lblValNewFirstname = new System.Windows.Forms.Label();
            this.txtEmailOpt2 = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblValNewPass = new System.Windows.Forms.Label();
            this.txtFirstname = new System.Windows.Forms.TextBox();
            this.txtEmailOpt1 = new System.Windows.Forms.TextBox();
            this.txtLastname = new System.Windows.Forms.TextBox();
            this.cmbStates = new System.Windows.Forms.ComboBox();
            this.btnCloseForm = new UI_Presentation.Controls.ButtonCloseForm();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsersAccounts)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.lblFormTitle.Location = new System.Drawing.Point(12, 25);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(400, 33);
            this.lblFormTitle.TabIndex = 37;
            this.lblFormTitle.Text = "MODIFICACIÓN DE USUARIOS";
            // 
            // dgvUsersAccounts
            // 
            this.dgvUsersAccounts.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgvUsersAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsersAccounts.Location = new System.Drawing.Point(15, 136);
            this.dgvUsersAccounts.Name = "dgvUsersAccounts";
            this.dgvUsersAccounts.RowHeadersWidth = 51;
            this.dgvUsersAccounts.Size = new System.Drawing.Size(283, 369);
            this.dgvUsersAccounts.TabIndex = 38;
            this.dgvUsersAccounts.SelectionChanged += new System.EventHandler(this.dgvUsersAccounts_SelectionChanged);
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.lblUsername.Location = new System.Drawing.Point(50, 7);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(108, 23);
            this.lblUsername.TabIndex = 40;
            this.lblUsername.Text = "Username";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.label1.Location = new System.Drawing.Point(58, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 42;
            this.label1.Text = "Password";
            // 
            // lblFirstname
            // 
            this.lblFirstname.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFirstname.AutoSize = true;
            this.lblFirstname.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.lblFirstname.Location = new System.Drawing.Point(51, 143);
            this.lblFirstname.Name = "lblFirstname";
            this.lblFirstname.Size = new System.Drawing.Size(107, 23);
            this.lblFirstname.TabIndex = 44;
            this.lblFirstname.Text = "Nombre/s";
            // 
            // lblLastname
            // 
            this.lblLastname.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLastname.AutoSize = true;
            this.lblLastname.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.lblLastname.Location = new System.Drawing.Point(48, 211);
            this.lblLastname.Name = "lblLastname";
            this.lblLastname.Size = new System.Drawing.Size(110, 23);
            this.lblLastname.TabIndex = 46;
            this.lblLastname.Text = "Apellido/s";
            // 
            // lblEmailOpt1
            // 
            this.lblEmailOpt1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblEmailOpt1.AutoSize = true;
            this.lblEmailOpt1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailOpt1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.lblEmailOpt1.Location = new System.Drawing.Point(96, 279);
            this.lblEmailOpt1.Name = "lblEmailOpt1";
            this.lblEmailOpt1.Size = new System.Drawing.Size(62, 23);
            this.lblEmailOpt1.TabIndex = 48;
            this.lblEmailOpt1.Text = "Email";
            // 
            // lblEmailOpt2
            // 
            this.lblEmailOpt2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblEmailOpt2.AutoSize = true;
            this.lblEmailOpt2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailOpt2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.lblEmailOpt2.Location = new System.Drawing.Point(3, 347);
            this.lblEmailOpt2.Name = "lblEmailOpt2";
            this.lblEmailOpt2.Size = new System.Drawing.Size(155, 23);
            this.lblEmailOpt2.TabIndex = 50;
            this.lblEmailOpt2.Text = "Email opcional";
            // 
            // lblState
            // 
            this.lblState.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.lblState.Location = new System.Drawing.Point(84, 408);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(74, 15);
            this.lblState.TabIndex = 51;
            this.lblState.Text = "Estado";
            // 
            // btnConfirmChanges
            // 
            this.btnConfirmChanges.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnConfirmChanges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.btnConfirmChanges.FlatAppearance.BorderSize = 0;
            this.btnConfirmChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmChanges.Image = global::UI_Presentation.Properties.Resources.dk_casilla_de_verificación_marcada_24;
            this.btnConfirmChanges.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirmChanges.Location = new System.Drawing.Point(300, 517);
            this.btnConfirmChanges.Name = "btnConfirmChanges";
            this.btnConfirmChanges.Size = new System.Drawing.Size(250, 50);
            this.btnConfirmChanges.TabIndex = 53;
            this.btnConfirmChanges.Text = "Confirmar cambios";
            this.btnConfirmChanges.UseVisualStyleBackColor = true;
            this.btnConfirmChanges.Click += new System.EventHandler(this.btnConfirmChanges_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(92)))));
            this.lblSearch.Location = new System.Drawing.Point(11, 85);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(78, 23);
            this.lblSearch.TabIndex = 54;
            this.lblSearch.Text = "Buscar:";
            // 
            // txtUserSearch
            // 
            this.txtUserSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtUserSearch.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserSearch.Location = new System.Drawing.Point(105, 82);
            this.txtUserSearch.Name = "txtUserSearch";
            this.txtUserSearch.Size = new System.Drawing.Size(193, 32);
            this.txtUserSearch.TabIndex = 55;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblUsername, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblValNewEmailOpt2, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.lblValNewUsername, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblValNewEmailOpt1, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtUsername, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblState, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.lblValNewLastname, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblValNewFirstname, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtEmailOpt2, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.txtPassword, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblEmailOpt2, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.lblValNewPass, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblFirstname, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtFirstname, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtEmailOpt1, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblLastname, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblEmailOpt1, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.txtLastname, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.cmbStates, 1, 13);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(304, 82);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 14;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(493, 423);
            this.tableLayoutPanel1.TabIndex = 62;
            // 
            // lblValNewEmailOpt2
            // 
            this.lblValNewEmailOpt2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblValNewEmailOpt2.AutoSize = true;
            this.lblValNewEmailOpt2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValNewEmailOpt2.ForeColor = System.Drawing.SystemColors.Control;
            this.lblValNewEmailOpt2.Location = new System.Drawing.Point(164, 384);
            this.lblValNewEmailOpt2.Name = "lblValNewEmailOpt2";
            this.lblValNewEmailOpt2.Size = new System.Drawing.Size(80, 17);
            this.lblValNewEmailOpt2.TabIndex = 61;
            this.lblValNewEmailOpt2.Text = "* opcional";
            this.lblValNewEmailOpt2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblValNewUsername
            // 
            this.lblValNewUsername.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblValNewUsername.AutoSize = true;
            this.lblValNewUsername.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValNewUsername.ForeColor = System.Drawing.SystemColors.Control;
            this.lblValNewUsername.Location = new System.Drawing.Point(164, 44);
            this.lblValNewUsername.Name = "lblValNewUsername";
            this.lblValNewUsername.Size = new System.Drawing.Size(92, 17);
            this.lblValNewUsername.TabIndex = 57;
            this.lblValNewUsername.Text = "* obligatorio";
            this.lblValNewUsername.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblValNewEmailOpt1
            // 
            this.lblValNewEmailOpt1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblValNewEmailOpt1.AutoSize = true;
            this.lblValNewEmailOpt1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValNewEmailOpt1.ForeColor = System.Drawing.SystemColors.Control;
            this.lblValNewEmailOpt1.Location = new System.Drawing.Point(164, 316);
            this.lblValNewEmailOpt1.Name = "lblValNewEmailOpt1";
            this.lblValNewEmailOpt1.Size = new System.Drawing.Size(92, 17);
            this.lblValNewEmailOpt1.TabIndex = 60;
            this.lblValNewEmailOpt1.Text = "* obligatorio";
            this.lblValNewEmailOpt1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsername.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(164, 3);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(355, 32);
            this.txtUsername.TabIndex = 39;
            this.txtUsername.Validating += new System.ComponentModel.CancelEventHandler(this.txtUsername_Validating);
            // 
            // lblValNewLastname
            // 
            this.lblValNewLastname.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblValNewLastname.AutoSize = true;
            this.lblValNewLastname.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValNewLastname.ForeColor = System.Drawing.SystemColors.Control;
            this.lblValNewLastname.Location = new System.Drawing.Point(164, 248);
            this.lblValNewLastname.Name = "lblValNewLastname";
            this.lblValNewLastname.Size = new System.Drawing.Size(92, 17);
            this.lblValNewLastname.TabIndex = 59;
            this.lblValNewLastname.Text = "* obligatorio";
            this.lblValNewLastname.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblValNewFirstname
            // 
            this.lblValNewFirstname.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblValNewFirstname.AutoSize = true;
            this.lblValNewFirstname.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValNewFirstname.ForeColor = System.Drawing.SystemColors.Control;
            this.lblValNewFirstname.Location = new System.Drawing.Point(164, 180);
            this.lblValNewFirstname.Name = "lblValNewFirstname";
            this.lblValNewFirstname.Size = new System.Drawing.Size(92, 17);
            this.lblValNewFirstname.TabIndex = 58;
            this.lblValNewFirstname.Text = "* obligatorio";
            this.lblValNewFirstname.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtEmailOpt2
            // 
            this.txtEmailOpt2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmailOpt2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailOpt2.Location = new System.Drawing.Point(164, 343);
            this.txtEmailOpt2.Name = "txtEmailOpt2";
            this.txtEmailOpt2.Size = new System.Drawing.Size(355, 32);
            this.txtEmailOpt2.TabIndex = 49;
            this.txtEmailOpt2.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmailOpt2_Validating);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(164, 71);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(355, 32);
            this.txtPassword.TabIndex = 41;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtPassword_Validating);
            // 
            // lblValNewPass
            // 
            this.lblValNewPass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblValNewPass.AutoSize = true;
            this.lblValNewPass.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValNewPass.ForeColor = System.Drawing.SystemColors.Control;
            this.lblValNewPass.Location = new System.Drawing.Point(164, 112);
            this.lblValNewPass.Name = "lblValNewPass";
            this.lblValNewPass.Size = new System.Drawing.Size(92, 17);
            this.lblValNewPass.TabIndex = 56;
            this.lblValNewPass.Text = "* obligatorio";
            this.lblValNewPass.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtFirstname
            // 
            this.txtFirstname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFirstname.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstname.Location = new System.Drawing.Point(164, 139);
            this.txtFirstname.Name = "txtFirstname";
            this.txtFirstname.Size = new System.Drawing.Size(355, 32);
            this.txtFirstname.TabIndex = 43;
            this.txtFirstname.Validating += new System.ComponentModel.CancelEventHandler(this.txtFirstname_Validating);
            // 
            // txtEmailOpt1
            // 
            this.txtEmailOpt1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmailOpt1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailOpt1.Location = new System.Drawing.Point(164, 275);
            this.txtEmailOpt1.Name = "txtEmailOpt1";
            this.txtEmailOpt1.Size = new System.Drawing.Size(355, 32);
            this.txtEmailOpt1.TabIndex = 47;
            this.txtEmailOpt1.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmailOpt1_Validating);
            // 
            // txtLastname
            // 
            this.txtLastname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLastname.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastname.Location = new System.Drawing.Point(164, 207);
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(355, 32);
            this.txtLastname.TabIndex = 45;
            this.txtLastname.Validating += new System.ComponentModel.CancelEventHandler(this.txtLastname_Validating);
            // 
            // cmbStates
            // 
            this.cmbStates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStates.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStates.FormattingEnabled = true;
            this.cmbStates.Location = new System.Drawing.Point(164, 411);
            this.cmbStates.Name = "cmbStates";
            this.cmbStates.Size = new System.Drawing.Size(355, 31);
            this.cmbStates.TabIndex = 52;
            this.cmbStates.SelectedIndexChanged += new System.EventHandler(this.cmbStates_SelectedIndexChanged);
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseForm.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseForm.Location = new System.Drawing.Point(788, 13);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(50, 48);
            this.btnCloseForm.TabIndex = 0;
            // 
            // FrmUsersEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(53)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(850, 579);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.txtUserSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.btnConfirmChanges);
            this.Controls.Add(this.dgvUsersAccounts);
            this.Controls.Add(this.lblFormTitle);
            this.Controls.Add(this.btnCloseForm);
            this.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUsersEdit";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmUsersEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsersAccounts)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ButtonCloseForm btnCloseForm;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.DataGridView dgvUsersAccounts;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFirstname;
        private System.Windows.Forms.Label lblLastname;
        private System.Windows.Forms.Label lblEmailOpt1;
        private System.Windows.Forms.Label lblEmailOpt2;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Button btnConfirmChanges;
        private Label lblSearch;
        private TextBox txtUserSearch;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblValNewEmailOpt2;
        private Label lblValNewUsername;
        private Label lblValNewEmailOpt1;
        private TextBox txtUsername;
        private Label lblValNewLastname;
        private Label lblValNewFirstname;
        private TextBox txtEmailOpt2;
        private TextBox txtPassword;
        private Label lblValNewPass;
        private TextBox txtFirstname;
        private TextBox txtEmailOpt1;
        private TextBox txtLastname;
        private ComboBox cmbStates;
    }
}