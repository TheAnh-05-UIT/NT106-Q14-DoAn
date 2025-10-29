using Newtonsoft.Json;
using QuanLyQuanNet.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08
{
    public partial class frm_Login : Form
    {
        public frm_Login()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string username = txt_Username.Text.Trim();
            string password = txt_Password.Text.Trim();

            var req = new
            {
                action = "login",
                username = username,
                password = password
            };

            string json = JsonConvert.SerializeObject(req);
            DAO.RequestData rd = new DAO.RequestData();
            string res = rd.SendRequest(json);

            dynamic obj = JsonConvert.DeserializeObject(res);
            if (obj.status == "success")
            {
                string role = obj.role;
                MessageBox.Show($"Đăng nhập thành công với quyền: {role}");

                if (role == "ADMIN") new ClientAdmin.frm_Admin_Employee_management().Show();
                else if (role == "EMPLOYEE") new ClientStaff.frm_Staff().Show();
                else new ClientCustomer.frm_Customer().Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }

        private void frm_Login_Load(object sender, EventArgs e)
        {

        }
    }
}



