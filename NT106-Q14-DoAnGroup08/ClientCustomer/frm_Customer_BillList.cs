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
    public partial class frm_Customer_BillList : CustomForm
    {
        private readonly ApiClient api = new ApiClient("127.0.0.1", 8080);
        public frm_Customer_BillList()
        {
            InitializeComponent();
        }

        private void guna2PictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Customer_BillList_Load(object sender, EventArgs e)
        {
            LoadInvoicesInSession();
        }

        private void LoadInvoicesInSession()
        {
            var res = api.Send(new { action = "get_invoices_in_session"});
            if (res == null || res.status != "success")
            {
                MessageBox.Show("Không tồn tại hoặc không tải được hóa đơn!");
                return;
            }

            JArray arr = (JArray)res.data;
            dataGridView1.Controls.Clear();

            DataTable dt = new DataTable();
            dt.Columns.Add("Invoice Id");
            dt.Columns.Add("Create At");
            dt.Columns.Add("Total Amount");

            foreach (var item in arr)
            {
                dt.Rows.Add
                (
                    item["InvoiceId"].ToString(),
                    item["CreateAt"].ToString(),
                    item["TotalAmount"].ToString()
                );
            }
            dataGridView1.DataSource = dt;

            AddDetailButton();
        }

        private void AddDetailButton()
        {
            if (dataGridView1.Columns.Contains("btnDetail"))
            {
                return;
            }
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "btnDetail";
            btn.HeaderText = "Detail";
            btn.Text = "+";
            btn.Width = 50;
            btn.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.Add(btn);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                return;
            }
            if(dataGridView1.Columns[e.ColumnIndex].Name == "btnDetail")
            {
                string invoiceId = dataGridView1.Rows[e.RowIndex].Cells["invoiceId"].Value.ToString();
                frm_Customer_BillDetail detail = new frm_Customer_BillDetail(invoiceId);
                detail.ShowDialog();
            }
        }
    }
}
