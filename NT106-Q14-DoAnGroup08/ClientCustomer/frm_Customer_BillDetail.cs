using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    public partial class frm_Customer_BillDetail : Form
    {
        private readonly ApiClient api = new ApiClient("127.0.0.1", 8080);
        private readonly string invoiceId;
        public frm_Customer_BillDetail(string invoiceId)
        {
            InitializeComponent();
            this.invoiceId = invoiceId;
        }

        private void frm_Customer_BillDetail_Load(object sender, EventArgs e)
        {
            var res = api.Send(new { action = "get_invoice_details", invoiceId = this.invoiceId });

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

            foreach (var item in arr)
            {
                dt.Rows.Add(
                    item["FoodName"].ToString(),
                    item["Quantity"].ToString(),
                    item["Price"].ToString(),
                    item["Total"].ToString()
                );
            }

            dataGridViewDetail.DataSource = dt;
        }
    }
}
