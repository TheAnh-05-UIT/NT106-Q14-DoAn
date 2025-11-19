using Newtonsoft.Json;
using NT106_Q14_DoAnGroup08.ConnectionServser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    public partial class frm_AddCustomer : Form
    {
        private bool isEditMode = false;
        public frm_AddCustomer()
        {
            InitializeComponent();
            isEditMode = false;
            this.Text = "Thêm khách hàng mới";
        }
        public frm_AddCustomer(dynamic data) : this()
        {
            isEditMode = true;
            this.Text = "Cập nhật thông tin khách hàng";

            // lấy dữ liệu cũ lên form
            txtHoten.Text = data.FullName;
            txtSodu.Text = data.Balance.ToString("N0").Replace(".", "");
            txtTenDangNhap.Text = data.Username;
            txtMatKhau.Text = data.Password;

            cbbTrangthai.SelectedItem = data.Status;

            // Khi sửa, không cho đổi Tên đăng nhập (khóa để tìm trong DB)
            txtTenDangNhap.Enabled = false;
        }
        private void frm_AddCustomer_Load(object sender, EventArgs e)
        {
            if (!isEditMode)
            {
                cbbTrangthai.SelectedIndex = 0;
                txtSodu.Text = "0";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoten.Text.Trim();
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string trangThai = cbbTrangthai.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(hoTen) ||
                string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Họ tên, Tên đăng nhập và Mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtSodu.Text, out decimal soDu) || soDu < 0)
            {
                MessageBox.Show("Số dư phải là một số không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo đối tượng data để gửi đi
            var customerData = new
            {
                fullName = txtHoten.Text.Trim(),
                balance = decimal.Parse(txtSodu.Text),
                status = cbbTrangthai.Text,
                username = txtTenDangNhap.Text.Trim(),
                password = txtMatKhau.Text.Trim()
            };

            // TỰ ĐỘNG CHỌN ACTION
            string actionName = isEditMode ? "UPDATE_CUSTOMER" : "ADD_CUSTOMER";

            var request = new
            {
                action = actionName,
                data = customerData
            };

            string jsonRequest = JsonConvert.SerializeObject(request);
            string jsonResponse = ServerConnection.SendRequest(jsonRequest);
            dynamic response = JsonConvert.DeserializeObject(jsonResponse);

            MessageBox.Show(response.message.ToString());

            if (response.status == "success")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCanCel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}