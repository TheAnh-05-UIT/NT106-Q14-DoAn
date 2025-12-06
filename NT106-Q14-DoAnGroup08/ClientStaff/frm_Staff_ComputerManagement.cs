using Guna.UI2.WinForms;
using Newtonsoft.Json;
using NT106_Q14_DoAnGroup08.ClientCustomer;
using NT106_Q14_DoAnGroup08.ConnectionServser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace NT106_Q14_DoAnGroup08.ClientStaff
{
    public partial class frm_Staff_ComputerManagement : Form
    {
        private string selectedComputerId = "";
        public frm_Staff_ComputerManagement()
        {
            InitializeComponent();
        }

        private void frm_Staff_ComputerManagement_Load(object sender, EventArgs e)
        {
            LoadComputerList();
        }

        private void BtnComputer_Click(object sender, EventArgs e)
        {
            Button clickedBtn = sender as Button;
            selectedComputerId = clickedBtn.Tag.ToString();
        }

        // CẬP NHẬT THỐNG KÊ (Active: x/y)
        // CẬP NHẬT THỐNG KÊ (Active / Available / Maintenance)
        private void UpdateStatistics(DataTable dt)
        {
            int countInUse = 0;      // Đang chơi
            int countAvailable = 0;  // Máy trống
            int countMaintenance = 0;// Bảo trì

            // 2. Duyệt qua từng dòng dữ liệu để đếm
            foreach (DataRow row in dt.Rows)
            {
                string status = row["Status"].ToString();

                switch (status)
                {
                    case "IN_USE":
                        countInUse++;
                        break;
                    case "AVAILABLE":
                        countAvailable++;
                        break;
                    case "MAINTENANCE":
                        countMaintenance++;
                        break;
                }
            }

            lbl_IN_USE.Text = $"IN USE: {countInUse}";
            lbl_AVAILABLE.Text = $"AVAILABLE: {countAvailable}";
            lbl_MAINTENANCE.Text= $"MAINTENANCE: {countMaintenance}";
        }

        // Hàm chung để gọi Server update trạng thái
        private void UpdateComputerStatus(string compId, string newStatus)
        {
            var request = new
            {
                action = "UPDATE_COMPUTER_STATUS",
                data = new { ComputerId = compId, Status = newStatus }
            };

            string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
            dynamic response = JsonConvert.DeserializeObject(jsonResponse);

            if (response.status == "success")
            {
                MessageBox.Show("Cập nhật trạng thái thành công!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadComputerList();
            }
            else
            {
                MessageBox.Show("Lỗi: " + response.message);
            }
        }

        // --- HÀM TẠO GIAO DIỆN MÁY (DYNAMIC BUTTONS) ---
        private void GenerateComputerControls(DataTable dt)
        {
            flpComputers.Controls.Clear(); // Xóa cũ
            selectedComputerId = ""; // Reset chọn

            foreach (DataRow row in dt.Rows)
            {
                // Tạo một nút đại diện cho máy
                Button btn = new Button();
                btn.Width = 100;
                btn.Height = 100;
                btn.Text = row["ComputerName"].ToString() + "\n" + row["Status"].ToString();
                btn.Tag = row["ComputerId"].ToString(); // Lưu ID máy vào Tag
                btn.Font = new Font("Arial", 10, FontStyle.Bold);
                btn.FlatStyle = FlatStyle.Flat;

                // màu dựa theo trạng thái
                string status = row["Status"].ToString();
                switch (status)
                {
                    case "AVAILABLE":
                        btn.BackColor = Color.LightGreen;
                        break;
                    case "IN_USE":
                        btn.BackColor = Color.IndianRed;
                        btn.ForeColor = Color.White;
                        break;
                    case "MAINTENANCE":
                        btn.BackColor = Color.Gray;
                        btn.ForeColor = Color.White;
                        break;
                }

                btn.Click += BtnComputer_Click;

                flpComputers.Controls.Add(btn);
            }
        }

        // --- HÀM TẢI DANH SÁCH MÁY TỪ SERVER ---
        private void LoadComputerList()
        {
            try
            {
                // 1. Gửi yêu cầu lấy danh sách
                var request = new { action = "GET_ALL_COMPUTERS" };
                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);

                if (response.status == "success")
                {
                    DataTable dt = response.data.ToObject<DataTable>();
                    GenerateComputerControls(dt);
                    UpdateStatistics(dt);
                }
                else
                {
                    MessageBox.Show("Lỗi tải danh sách máy: " + response.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadComputerList();
        }

        private void btnKhoaMay_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(selectedComputerId))
            {
                MessageBox.Show("Vui lòng chọn một máy để khóa.", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn muốn chuyển máy này sang trạng thái BẢO TRÌ?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                UpdateComputerStatus(selectedComputerId, "MAINTENANCE");
            }
        }

        private void btnMoMay_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedComputerId))
            {
                MessageBox.Show("Vui lòng chọn một máy để mở khóa.", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn muốn chuyển máy này sang trạng thái HOẠT ĐỘNG?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                UpdateComputerStatus(selectedComputerId, "AVAILABLE");
            }
        }
        private void btnBatDauPhien_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedComputerId))
            {
                MessageBox.Show("Vui lòng chọn máy để bắt đầu phiên chơi.", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            UpdateComputerStatus(selectedComputerId, "IN_USE");
        }
        private void btnKetThucPhien_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedComputerId))
            {
                MessageBox.Show("Vui lòng chọn máy đang chơi để kết thúc phiên chơi.", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            UpdateComputerStatus(selectedComputerId, "AVAILABLE");
        }
    }
}