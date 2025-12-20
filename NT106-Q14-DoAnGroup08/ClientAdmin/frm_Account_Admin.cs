using System;
using System.Drawing;
using System.Windows.Forms;

namespace NewNet_Manager.ClientAdmin
{
    public partial class frm_Account_Admin : Form
    {
        private string currentUserId;
        private string currentUserName;

        public frm_Account_Admin()
        {
            InitializeComponent(); this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

        }

        private void frm_Account_Admin_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(currentUserId))
            {
                lblUserNameVer2.Text = currentUserId;
                lblFullNameVer2.Text = currentUserName;

            }

            else
            {
                lblUserNameVer2.Text = "(Lỗi tải thông tin)";
                lblFullNameVer2.Text = "";
            }
        }
        public void changeLblTitle(string uname, string fullname)
        {
            currentUserId = uname;
            currentUserName = fullname;
            lblUserNameVer2.Text = uname;
            lblFullNameVer2.Text = fullname;
        }
    }

}