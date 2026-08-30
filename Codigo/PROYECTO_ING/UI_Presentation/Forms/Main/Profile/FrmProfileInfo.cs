using BE_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SERVICES.UserSession;
using SERVICES;
using BE_Entities.Users;

namespace UI_Presentation.Forms.Profile
{
    public partial class FrmProfileInfo : Form
    {
        public FrmProfileInfo()
        {
            InitializeComponent();
        }

        #region LOAD
        private void FrmProfileInfo_Load(object sender, EventArgs e)
        {
            lblDataUsername.Text = UserSession.GetInstance().UserAccount.Username.ToString();
            lblDataFirstname.Text = UserSession.GetInstance().UserAccount.FirstName.ToString();
            lblDataLastname.Text = UserSession.GetInstance().UserAccount.Lastname.ToString();
            lblDataEmailOpt1.Text = UserSession.GetInstance().UserAccount.EmailOption1.ToString();
            lblDataEmailOpt2.Text = UserSession.GetInstance().UserAccount.EmailOption2.ToString();
        }

        #endregion

        #region BOTONES
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
