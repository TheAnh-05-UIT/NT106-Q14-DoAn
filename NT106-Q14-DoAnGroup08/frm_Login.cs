using Newtonsoft.Json;
using NT106_Q14_DoAnGroup08.ConnectionServser;
using QuanLyQuanNet.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08
{
    public partial class frm_Login : Form
    {
        public delegate void LoginSuccessHandler();
        public event LoginSuccessHandler LoginSuccess;
        public frm_Login()
        {
            InitializeComponent(); this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string username = txt_Username.Text.Trim();
            string password = txt_Password.Text.Trim();

            // Tạo request JSON
            var req = new
            {
                action = "LOGIN",
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

                        SessionManager.Username = obj.userId;
                        SessionManager.Role = obj.role;
                        SessionManager.FullName = obj.fullName;

                        DTO.UserSession.UserId = obj.userId;
                        DTO.UserSession.Role = obj.role;
                        DTO.UserSession.FullName = obj.fullName;

                        string role = obj.role;

                        // Mở form theo role
                        if (role == "ADMIN")
                        {
                            MessageBox.Show($"Đăng nhập thành công với quyền: {role}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ClientAdmin.Admin adminForm = new ClientAdmin.Admin(obj.userId, obj.fullName);
                            DTO.UserSession.NextForm = adminForm;
                            //ApiClient.Client.Send(new
                            //{
                            //    action = "admin_online",
                            //    adminId = obj.userId
                            //});
                            LoginSuccess?.Invoke();
                            this.Close();
                        }
                        else if (role == "EMPLOYEE")
                        {
                            MessageBox.Show($"Đăng nhập thành công với quyền: {role}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ClientStaff.frm_Staff staffForm = new ClientStaff.frm_Staff(obj.userId);
                            DTO.UserSession.NextForm = staffForm;
                            ApiClient.Client.Send(new
                            {
                                action = "staff_online",
                                staffId = obj.userId
                            });
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show(obj.message ?? "Sai tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
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
            public string userId { get; set; }
            public string fullName { get; set; }

            public decimal? balance { get; set; }
        }
        private void UpdatePasswordMask()
        {
            string placeholder = txt_Password.Tag?.ToString();
            if (txt_Password.Text == placeholder)
            {
                txt_Password.UseSystemPasswordChar = false;
                return;
            }
            if (checkBoxVisible.Checked)
            {
                txt_Password.UseSystemPasswordChar = false;
            }
            else
            {
                txt_Password.UseSystemPasswordChar = true;
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null) return;

            string placeholder = tb.Tag?.ToString();
            if (tb.Text == placeholder)
            {
                tb.Text = "";
                tb.ForeColor = SystemColors.WindowText;
            }

            if (tb == txt_Password)
                UpdatePasswordMask();
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null) return;

            string placeholder = tb.Tag?.ToString();
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = placeholder;
                tb.ForeColor = SystemColors.GrayText;
            }

            if (tb == txt_Password)
                UpdatePasswordMask();
        }

        private void frm_Login_Load(object sender, EventArgs e)
        {

        }

        private void txt_Password_TextChanged(object sender, EventArgs e)
        {
            txt_Password.UseSystemPasswordChar = true;
        }

        private void pictureBoxUser_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Title_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Username_Click(object sender, EventArgs e)
        {

        }

        private void txt_Username_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_Password_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxVisible_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePasswordMask();
        }

    }
}



