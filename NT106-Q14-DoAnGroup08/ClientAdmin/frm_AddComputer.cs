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
    public partial class frm_AddComputer : Form
    {
        public frm_AddComputer()
        {
            InitializeComponent();
        }
        //public class Computer
        //{
        //    public string ComputerId { get; set; }
        //    public string ComputerName { get; set; }
        //    public string Status { get; set; }
        //    public string IpAddress { get; set; }
        //    public decimal PricePerHour { get; set; }
        //}
        //private void ChangGeForm(string computerId, string ComputerName, string Status, string IpAddress, decimal Price)
        //{
        //     computerId = txtID.Text;
        //     ComputerName = txtName.Text;
        //     Status = cobStatus.Text ;
        //     IpAddress = txtIP.Text;
        //     Price = decimal.Parse(txtPrice.Text);
        //}
        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //chua hoan thien
            try
            {
                var request = new
                {
                    action = "ADD_COMPUTER",
                    data = new
                    {
                        ComputerId = txtID.Text.Trim(),
                        ComputerName = txtName.Text.Trim(),
                        Status = cobStatus.Text.Trim(),
                        IpAddress = txtIP.Text.Trim(),
                        PricePerHour = decimal.Parse(txtPrice.Text.Trim())
                    }
                };
                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);
                if (response.status == "success")
                {
                    MessageBox.Show("Thêm máy thành công!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lỗi: " + response.message);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm máy tính: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
