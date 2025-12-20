using Newtonsoft.Json;
using NT106_Q14_DoAnGroup08.ConnectionServser;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.ClientAdmin
{
    public partial class Admin_CustomerAccountManagement : Form
    {
        public Admin_CustomerAccountManagement()
        {
            InitializeComponent(); this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void LoadCustomerData()
        {
            try
            {
                var request = new { action = "GET_ALL_CUSTOMERS" };
                string jsonRequest = JsonConvert.SerializeObject(request);

                string jsonResponse = ServerConnection.SendRequest(jsonRequest);

                // Sử dụng dynamic để dễ parse phản hồi
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);

                if (response.status == "success")
                {
                    // Chuyển đổi response.data thành DataTable
                    DataTable dt = response.data.ToObject<DataTable>();

                    // Gán DataTable làm nguồn dữ liệu cho DataGridView
                    dgvAccCustomers.DataSource = dt;
                    if (dgvAccCustomers.Columns.Contains("CustomerID"))
                    {
                        dgvAccCustomers.Columns["CustomerID"].Visible = false;
                    }
                    dgvAccCustomers.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
                else
                {
                    MessageBox.Show("Lỗi tải danh sách khách hàng: " + response.message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc xử lý dữ liệu: " + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Admin_CustomerAccountManagement_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
        }
        // Thêm customer 
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            using (var frm = new frm_AddCustomer())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadCustomerData();
                }
            }
        }
        // Sửa customer 
        private void btnRepair_Click(object sender, EventArgs e)
        {
            if (dgvAccCustomers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa.");
                return;
            }

            var drv = dgvAccCustomers.CurrentRow.DataBoundItem as DataRowView;
            if (drv == null) return;

            // Tạo dữ liệu để truyền đi
            var customerToEdit = new
            {
                FullName = drv["Họ tên"].ToString(),
                Balance = Convert.ToDecimal(drv["Số dư"]),
                Status = drv["Trạng thái"].ToString(),
                Username = drv["Tên đăng nhập"].ToString(),
                Password = drv["Mật khẩu"].ToString()
            };

            using (var frm = new frm_AddCustomer(customerToEdit))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadCustomerData();
                }
            }
        }
        // Xóa Customer 
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào đang chọn không
            if (dgvAccCustomers.CurrentRow == null) return;

            var drv = dgvAccCustomers.CurrentRow.DataBoundItem as DataRowView;

            // Kiểm tra nếu click vào dòng trắng cuối cùng
            if (drv == null) return;
            string customerId = drv["CustomerID"].ToString();
            string customerName = drv["Họ tên"].ToString();

            if (MessageBox.Show($"Bạn có chắc muốn xóa khách hàng '{customerName}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var request = new { action = "DELETE_CUSTOMER", data = new { customerId = customerId } };
                string jsonRequest = JsonConvert.SerializeObject(request);
                string jsonResponse = ServerConnection.SendRequest(jsonRequest);

                dynamic response = JsonConvert.DeserializeObject(jsonResponse);
                MessageBox.Show(response.message.ToString());

                if (response.status == "success")
                {
                    LoadCustomerData();
                }
            }
        }
        // nạp tiền
        private void btnNaptien_Click(object sender, EventArgs e)
        {
            if (dgvAccCustomers.CurrentRow == null) return;

            var drv = dgvAccCustomers.CurrentRow.DataBoundItem as DataRowView;

            // Kiểm tra null để tránh lỗi crash
            if (drv == null) return;

            var customerData = new
            {
                Username = drv["Tên đăng nhập"].ToString(),
                Status = drv["Trạng thái"].ToString(),
                Balance = Convert.ToDecimal(drv["Số dư"])
            };

            // Mở form nạp tiền
            using (var frm = new frm_Deposit(customerData))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadCustomerData();
                }
            }
        }
        // Tải lại dữ liệu
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomerData();
        }
        // Tìm kiếm dữ liệu
        private void SearchCustomers(string keyword)
        {
            try
            {
                var request = new
                {
                    action = "SEARCH_CUSTOMER",
                    data = new { keyword = keyword }
                };

                string jsonRequest = JsonConvert.SerializeObject(request);
                string jsonResponse = ServerConnection.SendRequest(jsonRequest);

                dynamic response = JsonConvert.DeserializeObject(jsonResponse);

                if (response.status == "success")
                {
                    DataTable dt = response.data.ToObject<DataTable>();

                    dgvAccCustomers.AutoGenerateColumns = false;
                    dgvAccCustomers.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Lỗi tìm kiếm: " + response.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void pb_TimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            SearchCustomers(keyword);
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadCustomerData(); // Nếu xóa hết chữ thì tải lại danh sách đầy đủ
            }
            else
            {
                SearchCustomers(keyword);
            }
        }

        private void dgvAccCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

