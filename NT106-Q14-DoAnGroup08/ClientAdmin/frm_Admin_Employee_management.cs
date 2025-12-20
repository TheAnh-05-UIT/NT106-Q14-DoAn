using NewNet_Manager.ConnectionServser;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace NewNet_Manager.ClientAdmin
{
    public partial class frm_Admin_Employee_management : Form
    {
        public frm_Admin_Employee_management()
        {
            InitializeComponent(); this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }
        private void frm_Admin_Employee_management_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            var request = new
            {
                action = "get_all_employees"
            };

            string jsonRequest = JsonConvert.SerializeObject(request);
            string jsonResponse = ServerConnection.SendRequest(jsonRequest);

            try
            {
                var result = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                if (result.status == "success")
                {
                    dgvNhanVien.DataSource = null;
                    dgvNhanVien.Columns.Clear();
                    dgvNhanVien.AutoGenerateColumns = true;
                    dgvNhanVien.DataSource = result.data.ToObject<DataTable>();
                }
                else
                {
                    MessageBox.Show(result.message.ToString(), "Lỗi tải dữ liệu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi parse JSON: " + ex.Message);
            }
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            try
            {
                var newEmployee = new
                {
                    maNV = txtMaNV.Text,
                    hoTen = txtHoTen.Text,
                    gioiTinh = cboGioiTinh.Text,
                    ngaySinh = dtpNgaySinh.Value.ToString("yyyy-MM-dd"),
                    soDienThoai = txtSDT.Text,
                    ngayVaoLam = dtpNgayVaoLam.Value.ToString("yyyy-MM-dd"),
                    soNgayLam = (int)numSoNgayLam.Value,
                    luongCoBan = decimal.Parse(txtLuongCoBan.Text),
                    luongThang = decimal.Parse(txtLuongThang.Text)
                };

                var request = new
                {
                    action = "add_employee",
                    data = newEmployee
                };

                string jsonRequest = JsonConvert.SerializeObject(request);
                string jsonResponse = ServerConnection.SendRequest(jsonRequest);

                var result = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                MessageBox.Show(result.message.ToString(), result.status.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result.status == "success")
                    LoadEmployeeData();
            }
            catch
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                return;
            }
        }

        private void BTNSuaNV_Click(object sender, EventArgs e)
        {
            var updateEmployee = new
            {
                maNV = txtMaNV.Text,
                luongCoBan = decimal.Parse(txtLuongCoBan.Text),
                soNgayLam = (int)numSoNgayLam.Value
            };

            var request = new
            {
                action = "update_employee",
                data = updateEmployee
            };

            string jsonRequest = JsonConvert.SerializeObject(request);
            string jsonResponse = ServerConnection.SendRequest(jsonRequest);

            var result = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            MessageBox.Show(result.message.ToString(), result.status.ToString());

            if (result.status == "success")
                LoadEmployeeData();
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            var request = new
            {
                action = "delete_employee",
                data = new { maNV = txtMaNV.Text }
            };

            string jsonRequest = JsonConvert.SerializeObject(request);
            string jsonResponse = ServerConnection.SendRequest(jsonRequest);

            var result = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            MessageBox.Show(result.message.ToString(), result.status.ToString());

            if (result.status == "success")
                LoadEmployeeData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvNhanVien.SelectedRows[0];
            row.Cells["colID"].Value = txtMaNV.Text;
            row.Cells["colName"].Value = txtHoTen.Text;
            row.Cells["colSex"].Value = cboGioiTinh.Text;
            row.Cells["colDateOfBirth"].Value = dtpNgaySinh.Value.ToString("dd/MM/yyyy");
            row.Cells["colPhone"].Value = txtSDT.Text;
            row.Cells["colStartingDate"].Value = dtpNgayVaoLam.Value.ToString("dd/MM/yyyy");
            row.Cells["colNumberOfWorkingDays"].Value = numSoNgayLam.Value.ToString();
            row.Cells["colBasicSalary"].Value = txtLuongCoBan.Text;
            row.Cells["colMonthlySalary"].Value = txtLuongThang.Text;
            dgvNhanVien.EndEdit();
            btnSave.Visible = false;
            MessageBox.Show("Lưu thay đổi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells["Mã nhân viên"].Value.ToString();
                txtHoTen.Text = row.Cells["Họ tên"].Value.ToString();
                cboGioiTinh.Text = row.Cells["Giới tính"].Value.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["Ngày sinh"].Value);
                txtSDT.Text = row.Cells["Số điện thoại"].Value.ToString();
                dtpNgayVaoLam.Value = Convert.ToDateTime(row.Cells["Ngày vào làm"].Value);
                numSoNgayLam.Value = Convert.ToInt32(row.Cells["Số ngày làm"].Value);
                txtLuongCoBan.Text = row.Cells["Lương cơ bản"].Value.ToString();
                txtLuongThang.Text = row.Cells["Lương tháng"].Value.ToString();
            }
        }

        //public static implicit operator frm_Admin_Employee_management(Admin_CustomerAccountManagement v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
