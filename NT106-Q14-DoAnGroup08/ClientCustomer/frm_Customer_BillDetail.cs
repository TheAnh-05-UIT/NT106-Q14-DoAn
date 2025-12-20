using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    public partial class frm_Customer_BillDetail : Form
    {
        private readonly string invoiceId;
        public frm_Customer_BillDetail(string invoiceId)
        {
            InitializeComponent(); this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.invoiceId = invoiceId;
        }

        private void frm_Customer_BillDetail_Load(object sender, EventArgs e)
        {
            var res = ApiClient.Client.Send(new { action = "get_invoices_details", invoiceId = this.invoiceId });
            if (res == null || res.status != "success")
            {
                MessageBox.Show("Không tải được chi tiết!");
                return;
            }

            JArray arr = (JArray)res.data;

            DataTable dt = new DataTable();
            dt.Columns.Add("FoodName");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Price");
            dt.Columns.Add("Total");
            dt.Columns.Add("Note");


            foreach (var item in arr)
            {
                dt.Rows.Add(
                    item["FoodName"]?.ToString() ?? "",
                    item["Quantity"]?.ToString() ?? "0",
                    item["Price"]?.ToString() ?? "0",
                    item["Total"]?.ToString() ?? "0",
                    item["Note"]?.ToString() ?? ""
                );
            }

            dataGridViewDetail.DataSource = dt;
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
