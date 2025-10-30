﻿using Newtonsoft.Json;
using NT106_Q14_DoAnGroup08.ClientAdmin;
using NT106_Q14_DoAnGroup08.ConnectionServser;
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

            // Tạo request JSON
            var req = new
            {
                action = "login",
                username = username,
                password = password
            };

            string json = JsonConvert.SerializeObject(req);

            // Gửi request đến server
            string res = ServerConnection.SendRequest(json);

            // Kiểm tra phản hồi
            if (string.IsNullOrWhiteSpace(res))
            {
                MessageBox.Show("Không nhận được phản hồi từ server!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Nếu phản hồi là JSON hợp lệ
                if (res.Trim().StartsWith("{"))
                {
                    var obj = JsonConvert.DeserializeObject<LoginResponse>(res);

                    if (obj.status == "success")
                    {
                        string role = obj.role;
                        MessageBox.Show($"Đăng nhập thành công với quyền: {role}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Mở form theo role
                        if (role == "ADMIN")
                        {
                            ClientAdmin.frm_Admin_Employee_management Admin_Employee = new ClientAdmin.frm_Admin_Employee_management();
                            Admin_Employee.Show();
                            //this.Hide();
                        }   
                        else if (role == "EMPLOYEE")
                            new ClientStaff.frm_Staff().Show();
                        else
                            new ClientCustomer.frm_Customer().Show();
                    }
                    else
                    {
                        MessageBox.Show(obj.message ?? "Sai tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // phản hồi không phải JSON
                    MessageBox.Show($"Phản hồi từ server: {res}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (JsonReaderException)
            {
                MessageBox.Show($"Phản hồi không hợp lệ: {res}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // phản hồi JSON
        public class LoginResponse
        {
            public string status { get; set; }
            public string role { get; set; }
            public string message { get; set; }
        }

        private void frm_Login_Load(object sender, EventArgs e)
        {

        }
    }
}



