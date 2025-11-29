using Newtonsoft.Json;
using NT106_Q14_DoAnGroup08.ConnectionServser;
using QuanLyQuanNet.DTOs;
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
    public partial class frm_Admin_Change : Form
    {
        private string _computerId;

        // Form này chứa các Controls: txtID, txtName, cobStatus, txtIP, txtPrice

        // CONSTRUCTOR BẠN YÊU CẦU: Nhận ID máy
        public frm_Admin_Change(string computerId)
        {
            InitializeComponent();
            _computerId = computerId.Trim();

            // Khóa trường ID để người dùng không thể sửa khóa chính
            txtID.Text = _computerId;
            txtID.ReadOnly = true;

            // Cấu hình ComboBox cho Status (Đảm bảo giá trị khớp với DB)
            cobStatus.Items.AddRange(new string[] { "AVAILABLE", "IN_USE", "MAINTENANCE" });

            // Tải dữ liệu khi Form khởi tạo
            LoadComputerDetails();
        }

        // HÀM TẢI DỮ LIỆU
        private void LoadComputerDetails()
        {
            try
            {
                var request = new
                {
                    action = "GET_COMPUTER_DETAILS",
                    data = new { ComputerId = _computerId }
                };

                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);

                if (response.status == "success")
                {
                    dynamic computer = response.data;

                    // Gán dữ liệu vào các controls
                    txtName.Text = (string)computer.ComputerName;
                    cobStatus.Text = (string)computer.Status;
                    txtIP.Text = (string)computer.IpAddress;
                    txtPrice.Text = computer.PricePerHour.ToString();
                }
                else
                {
                    MessageBox.Show("Lỗi tải thông tin máy: " + response.message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close(); // Đóng nếu không tải được
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        // HÀM GỬI YÊU CẦU CẬP NHẬT KHI NHẤN NÚT LƯU
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Sử dụng .Trim() để loại bỏ khoảng trắng thừa từ đầu vào người dùng
                decimal pricePerHourValue;
                if (!decimal.TryParse(txtPrice.Text.Trim(), out pricePerHourValue))
                {
                    MessageBox.Show("Giá tiền không hợp lệ. Vui lòng kiểm tra lại.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Dừng hàm nếu nhập sai định dạng
                }

                var request = new
                {
                    action = "UPDATE_COMPUTER",
                    data = new
                    {
                        ComputerId = _computerId,
                        ComputerName = txtName.Text.Trim(),
                        Status = cobStatus.Text.Trim(),
                        IpAddress = txtIP.Text.Trim(),
                        PricePerHour = pricePerHourValue // Sử dụng giá trị đã Parse thành công
                    }
                };

                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);

                if (response.status == "success")
                {
                    MessageBox.Show("Cập nhật thông tin máy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lỗi cập nhật: " + response.message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Giá tiền không hợp lệ. Vui lòng nhập số.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật máy tính: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Bạn cần tự thêm hàm cho nút Hủy Bỏ (btnCancel_Click)
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }
    }
}