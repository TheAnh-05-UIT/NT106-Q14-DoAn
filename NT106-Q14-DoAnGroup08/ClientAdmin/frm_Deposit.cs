using Newtonsoft.Json;
using NewNet_Manager.ConnectionServser;
using NewNet_Manager.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NewNet_Manager.ClientAdmin
{
    public partial class frm_Deposit : Form
    {
        private dynamic customerData;

        public frm_Deposit()
        {
            InitializeComponent(); this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        public frm_Deposit(dynamic data)
        {
            InitializeComponent(); this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.customerData = data;
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtDeposit.Text, out decimal amountToDeposit) || amountToDeposit <= 0)
            {
                MessageBox.Show("Số tiền nạp phải là một số dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy tên người dùng từ dữ liệu đã lưu
            string username = this.customerData.Username;

            // Tạo yêu cầu JSON
            var request = new
            {
                action = "DEPOSIT_FUNDS",
                data = new
                {
                    username = username,
                    amount = amountToDeposit,
                    employeeId = UserSession.UserId
                }
            };
            string jsonRequest = JsonConvert.SerializeObject(request);

            // Gửi yêu cầu lên server
            string jsonResponse = ServerConnection.SendRequest(jsonRequest);
            dynamic response = JsonConvert.DeserializeObject(jsonResponse);

            MessageBox.Show(response.message.ToString(), "Thông báo");

            if (response.status == "success")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frm_Deposit_Load(object sender, EventArgs e)
        {
            if (this.customerData != null)
            {
                // Lấy dữ liệu từ customerData và gán vào các textbox
                txtUserName.Text = customerData.Username;
                txtStatus.Text = customerData.Status;
                txtBalance.Text = customerData.Balance.ToString("N0");

                // Khóa các ô này lại vì người dùng không được sửa
                txtUserName.Enabled = false;
                txtStatus.Enabled = false;
                txtBalance.Enabled = false;
            }
        }
    }
}
