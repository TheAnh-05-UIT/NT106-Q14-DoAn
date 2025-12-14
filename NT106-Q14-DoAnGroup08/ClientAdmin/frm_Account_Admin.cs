using QuanLyQuanNet.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.ClientAdmin
{
    public partial class frm_Account_Admin : Form
    {
        private string currentUserId;
        private string currentUserName;
        public frm_Account_Admin()
        {
            InitializeComponent();
        }

        private void frm_Account_Admin_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentUserId))
            {
                lblUserNameVer2.Text = currentUserId; // Hiển thị ID người dùng
                lblFullNameVer2.Text = currentUserName; // Hiển thị Tên đầy đủ
                //lblRole.Text = "ADMIN"; // Hoặc lấy từ SessionManager nếu SessionManager được cập nhật
            }
            // Logic cũ (chủ yếu cho Employee/Customer)
            else
            {
                var username = SessionManager.Username;
                if (!string.IsNullOrEmpty(username))
                {
                    lblUserNameVer2.Text = username;
                    lblFullNameVer2.Text = SessionManager.FullName ?? "";
             
                }
                else
                {
                    lblUserNameVer2.Text = "(chưa đăng nhập)";
                    lblFullNameVer2.Text = "";
                   
                }
            }
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (res == DialogResult.Yes)
            {
                try
                {
                    //ApiClient.Client.Send(new
                    //{
                    //    action = "staff_offline",
                    //    staffId = staffId
                    //});
                }
                catch { }

                //SessionManager.Clear();

                //LogoutClicked?.Invoke(this, EventArgs.Empty);
            }
        }
        public void changeLblTitle( string uname, string fullname)
        {
            currentUserId = uname;
            currentUserName = fullname;
            lblUserNameVer2.Text = uname;
            lblFullNameVer2.Text = fullname;
        }
    }
    
}
