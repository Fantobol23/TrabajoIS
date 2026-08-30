using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_Presentation.Controls
{
    public partial class ButtonCloseForm : UserControl
    {
        public ButtonCloseForm()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Form frmPadre = FindForm();
            frmPadre.Close();
            frmPadre.Dispose();
        }
    }
}
