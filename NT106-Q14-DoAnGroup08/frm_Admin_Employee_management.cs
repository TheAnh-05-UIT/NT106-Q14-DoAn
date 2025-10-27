using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
namespace NT106_Q14_DoAnGroup08
{
    public partial class frm_Admin_Employee_management : Form
    {
        public frm_Admin_Employee_management()
        {
            InitializeComponent();
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
           try
            {
                if(string.IsNullOrWhiteSpace(txtMaNV.Text) ||
                    string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                    string.IsNullOrWhiteSpace(cboGioiTinh.Text) ||
                    string.IsNullOrWhiteSpace(txtSDT.Text) ||
                    string.IsNullOrWhiteSpace(txtLuongCoBan.Text) ||
                    string.IsNullOrWhiteSpace(txtLuongThang.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin nhân viên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string ngaysinh = dtpNgaySinh.Value.ToString("dd/MM/yyyy");
                string ngayvaolam = dtpNgayVaoLam.Value.ToString("dd/MM/yyyy");
                dgvNhanVien.Rows.Add(txtMaNV.Text, txtHoTen.Text, cboGioiTinh.Text, ngaysinh, txtSDT.Text, ngayvaolam, numSoNgayLam.Value.ToString(), txtLuongCoBan.Text, txtLuongThang.Text);
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTNSuaNV_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                btnSave.Visible = true;
                DataGridViewRow row = dgvNhanVien.SelectedRows[0];
                txtMaNV.Text = row.Cells["colID"].Value?.ToString();
                txtHoTen.Text = row.Cells["colName"].Value?.ToString();
                cboGioiTinh.Text = row.Cells["colSex"].Value?.ToString();
                string ngaysinh = row.Cells["colDateOfBirth"].Value?.ToString();
                if (DateTime.TryParseExact(ngaysinh, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngays ))
                {
                   dtpNgaySinh.Value = ngays;
                }
                string ngayvaolam = row.Cells["colStartingDate"].Value?.ToString();
                if (DateTime.TryParseExact(ngayvaolam, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngayvl))
                {
                    dtpNgayVaoLam.Value = ngayvl;
                }
                txtSDT.Text = row.Cells["colPhone"].Value?.ToString();
                txtLuongCoBan.Text = row.Cells["colBasicSalary"].Value?.ToString();
                txtLuongThang.Text = row.Cells["colMonthlySalary"].Value?.ToString();
                numSoNgayLam.Value = Convert.ToDecimal(row.Cells["colNumberOfWorkingDays"].Value?.ToString());
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên muốn sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void frm_Admin_Employee_management_Load(object sender, EventArgs e)
        {
            dgvNhanVien.Rows.Add("NV01", "Nguyen Van A", "Nam", "01/01/1998", "0372773025", "25/08/2024", "300", "3000000","5000000");
            dgvNhanVien.Rows.Add("NV02", "Nguyen Van B", "Nu", "30/12/2000","0352653331", "25/08/2025", "12", "3000000", "5000000");
            dgvNhanVien.Rows.Add("NV03", "Nguyen Van C", "Nam", "06/07/2001" , "0333324943", "25/09/2024", "24", "3000000", "5000000");
            dgvNhanVien.Rows.Add("NV04", "Nguyen Van D", "Nam", "09/09/2002", "037256789", "15/08/2024", "48", "3000000", "5000000");
            dgvNhanVien.Rows.Add("NV05", "Nguyen Van E", "Nu",  "20/10/2003", "0123456789", "25/10/2024","96", "3000000", "5000000");
            dgvNhanVien.Rows.Add("NV06", "Nguyen Van F", "Nu",  "02/09/1945", "0233245678", "05/08/2025", "192", "3000000", "5000000");
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if(dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một hàng để xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(rs == DialogResult.Yes)
            {
                DataGridViewRow row = dgvNhanVien.SelectedRows[0];
                if(!row.IsNewRow)
                {
                    dgvNhanVien.Rows.Remove(row);
                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
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
    }
}
