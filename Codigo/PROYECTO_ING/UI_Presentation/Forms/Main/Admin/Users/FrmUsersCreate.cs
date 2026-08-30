using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_Presentation.Forms.Main;

namespace UI_Presentation.Forms.Admin
{
    public partial class FrmUsersCreate : Form
    {
        public FrmUsersCreate()
        {
            InitializeComponent();
        }

        #region LOAD/CARGA INICIAL
        private void FrmUsersCreate_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Text = string.Empty;
            this.ControlBox = false;
            this.Dock = DockStyle.Fill;
        }
        #endregion

        #region BOTONES
        private void btnCerrarForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region FUNCIONES

        #endregion

    }
}
