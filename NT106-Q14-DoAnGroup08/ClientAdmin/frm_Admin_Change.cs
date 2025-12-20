using NewNet_Manager.ConnectionServser;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NewNet_Manager.ClientAdmin
{
    public partial class frm_Admin_Change : Form
    {
        private string _computerId;
        public frm_Admin_Change(string computerId)
        {
            InitializeComponent(); this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            _computerId = computerId.Trim();
            txtID.Text = _computerId;
            txtID.ReadOnly = true;

            cobStatus.Items.AddRange(new string[] { "AVAILABLE", "IN_USE", "MAINTENANCE" });

            LoadComputerDetails();
        }

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

                    txtName.Text = (string)computer.ComputerName;
                    cobStatus.Text = (string)computer.Status;
                    txtIP.Text = (string)computer.IpAddress;
                    txtPrice.Text = computer.PricePerHour.ToString();
                }
                else
                {
                    MessageBox.Show("Lỗi tải thông tin máy: " + response.message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                decimal pricePerHourValue;
                if (!decimal.TryParse(txtPrice.Text.Trim(), out pricePerHourValue))
                {
                    MessageBox.Show("Giá tiền không hợp lệ. Vui lòng kiểm tra lại.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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
                        PricePerHour = pricePerHourValue
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}