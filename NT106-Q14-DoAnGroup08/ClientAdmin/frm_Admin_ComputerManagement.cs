using Newtonsoft.Json;
using NT106_Q14_DoAnGroup08.ConnectionServser;
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
    public partial class frm_Admin_ComputerManagement : Form
    {
        private string selectedComputerId = "";
        public frm_Admin_ComputerManagement()
        {
            InitializeComponent();
        }
       
        private void BtnComputer_Click(object sender, EventArgs e)
        {
            Button clickedBtn = sender as Button;
            selectedComputerId = clickedBtn.Tag.ToString();
        }
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
            lbl_MAINTENANCE.Text = $"MAINTENANCE: {countMaintenance}";
        }

        // Hàm chung để gọi Server update trạng thái
        //private void UpdateComputerStatus(string compId, string newStatus)
        //{
        //    var request = new
        //    {
        //        action = "UPDATE_COMPUTER_STATUS",
        //        data = new { ComputerId = compId, Status = newStatus }
        //    };

        //    string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
        //    dynamic response = JsonConvert.DeserializeObject(jsonResponse);

        //    if (response.status == "success")
        //    {
        //        MessageBox.Show("Cập nhật trạng thái thành công!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        LoadComputerList();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Lỗi: " + response.message);
        //    }
        //}

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
                var request = new { action = "GET_ALL_COMPUTERS_ADMIN" };
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
        private void DeleteComputer(string computerid)
        {
            try
            {
                var request = new
                {
                    action = "DELETE_COMPUTER",
                    data = new { ComputerId = computerid }
                };
                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);
                if (response.status == "success")
                {
                    MessageBox.Show("Xóa máy thành công!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadComputerList();
                }
                else
                {
                    MessageBox.Show("Lỗi: " + response.message);
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frm_AddComputer f = new frm_AddComputer();
            f.ShowDialog();
            LoadComputerList();
        }

        private void frm_Admin_ComputerManagement_Load(object sender, EventArgs e)
        {
            LoadComputerList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedComputerId))
            {
                MessageBox.Show("Vui lòng chọn máy để xóa.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(MessageBox.Show("Bạn muốn XÓA máy này ra khỏi hệ thống máy?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DeleteComputer(selectedComputerId);
            }
        }
    }
}
