using NT106_Q14_DoAnGroup08.ConnectionServser;
using NT106_Q14_DoAnGroup08.Uc_Staff;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.ClientAdmin
{
    public partial class Admin : Form
    {
        private string adminUserId;
        private string adminFullName;
        private string adminUserName;
        // private string RoleUser;
        public Admin(string userId, string fullName)
        {
            InitializeComponent(); this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            adminUserId = userId;
            adminFullName = fullName;
            getAdminInfo(adminUserId);
        }

        private void getAdminInfo(string adminUserId)
        {
            try
            {
                var request = new
                {
                    action = "GET_INFO_ADMIN",
                    data = new
                    {
                        userId = adminUserId
                    }
                };
                string jsonRequest = JsonConvert.SerializeObject(request);
                string jsonResponse = ServerConnection.SendRequest(jsonRequest);
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);
                if (response.status == "success")
                {
                    dynamic adminData = response.data;
                    adminUserName = adminData.UserName;
                    adminFullName = adminData.FullName;
                    //RoleUser = adminData.AdminRole;
                }
                else
                {
                    MessageBox.Show("Lỗi: " + response.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin quản lý: " + ex.Message);
            }
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            frm_Account_Admin f = new frm_Account_Admin();
            f.changeLblTitle(adminUserName, adminFullName);
            openChildForm(f);
        }
        private void openChildForm(Form childForm)
        {
            panelContainerAdmin.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelContainerAdmin.Controls.Add(childForm);
            childForm.Show();
        }

        private void OpenChildControl(Control child)
        {
            try
            {
                panelContainerAdmin.Controls.Clear();
                child.Dock = DockStyle.Fill;
                panelContainerAdmin.Controls.Add(child);
                child.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở control: " + ex.Message);
            }
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            OpenChildControl(new uc_Staff_ImportGood());
        }

        private void btnManagerCPT_Click(object sender, EventArgs e)
        {
            openChildForm(new Admin_CustomerAccountManagement());
        }

        private void btnManagerStaff_Click(object sender, EventArgs e)
        {
            openChildForm(new frm_Admin_Employee_management());
        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            openChildForm(new frm_Revenue());
        }

        private void btnChat_Click(object sender, EventArgs e)
        {

        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            frm_Account_Admin f = new frm_Account_Admin();
            f.changeLblTitle(adminUserName, adminFullName);
            openChildForm(f);
        }

        private void btnComPuMa_Click(object sender, EventArgs e)
        {
            openChildForm(new frm_Admin_ComputerManagement());
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
                    ClientCustomer.frm_LockScreen f = new ClientCustomer.frm_LockScreen();
                    this.Hide();
                    f.ShowDialog();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Lỗi khi đăng xuất: {ex.Message}",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }
    }
}