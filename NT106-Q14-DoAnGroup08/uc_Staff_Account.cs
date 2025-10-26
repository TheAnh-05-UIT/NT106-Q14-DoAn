using System;
using System.Windows.Forms;
using QuanLyQuanNet.Utils;

namespace NT106_Q14_DoAnGroup08
{
    public partial class uc_Staff_Account : UserControl
    {
        public event EventHandler LogoutClicked;

        public uc_Staff_Account()
        {
            InitializeComponent();
            this.Load += Uc_Staff_Account_Load;
            btnLogout.Click += BtnLogout_Click;
        }

        private void Uc_Staff_Account_Load(object sender, EventArgs e)
        {
            var username = SessionManager.Username;
            if (!string.IsNullOrEmpty(username))
            {
                lblCurrentUser.Text = username;
                lblFullName.Text = SessionManager.FullName ?? "";
                lblRole.Text = SessionManager.Role ?? "";
            }
            else
            {
                lblCurrentUser.Text = "(chưa đăng nhập)";
                lblFullName.Text = "";
                lblRole.Text = "";
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                SessionManager.Clear();
                LogoutClicked?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
