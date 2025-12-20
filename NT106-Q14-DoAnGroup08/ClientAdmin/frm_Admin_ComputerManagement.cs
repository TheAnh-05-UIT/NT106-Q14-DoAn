using Newtonsoft.Json;
using NT106_Q14_DoAnGroup08.ConnectionServser;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.ClientAdmin
{
    public partial class frm_Admin_ComputerManagement : Form
    {
        private string selectedComputerId = "";
        public frm_Admin_ComputerManagement()
        {
            InitializeComponent(); this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void BtnComputer_Click(object sender, EventArgs e)
        {
            Button clickedBtn = sender as Button;
            selectedComputerId = clickedBtn.Tag.ToString();
        }
        private void UpdateStatistics(DataTable dt)
        {
            int countInUse = 0;
            int countAvailable = 0;
            int countMaintenance = 0;
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
        private void GenerateComputerControls(DataTable dt)
        {
            flpComputers.Controls.Clear();
            selectedComputerId = "";

            foreach (DataRow row in dt.Rows)
            {
                Button btn = new Button();
                btn.Width = 100;
                btn.Height = 100;
                btn.Text = row["ComputerName"].ToString() + "\n" + row["Status"].ToString();
                btn.Tag = row["ComputerId"].ToString();
                btn.Font = new Font("Arial", 10, FontStyle.Bold);
                btn.FlatStyle = FlatStyle.Flat;
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
        private void LoadComputerList()
        {
            try
            {
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
            if (MessageBox.Show("Bạn muốn XÓA máy này ra khỏi hệ thống máy?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DeleteComputer(selectedComputerId);
            }
        }

        private void btnRepair_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedComputerId))
            {
                MessageBox.Show("Vui lòng chọn máy để sửa thông tin.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frm_Admin_Change f = new frm_Admin_Change(selectedComputerId);
            f.ShowDialog();
            LoadComputerList();
        }
    }
}
