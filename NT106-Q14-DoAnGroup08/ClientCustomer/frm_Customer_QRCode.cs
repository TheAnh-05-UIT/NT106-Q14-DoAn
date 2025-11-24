using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    public partial class frm_Customer_QRCode : Form
    {
        
        private string base64QR;
        int amount;

        public frm_Customer_QRCode(string base64, int Amount)
        {
            InitializeComponent();
            base64QR = base64;
            amount = Amount;
        }

        private void frm_Customer_QRCode_Load(object sender, EventArgs e)
        {
            picQR.Image = Base64ToImage(base64QR);
        }

        public Image Base64ToImage(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            var maxInv = ApiClient.Client.Send(new { action = "get_max_invoice_id" });
            string lastId = maxInv?["maxId"]?.ToString();

            string invoiceId;

            if (string.IsNullOrEmpty(lastId))
                invoiceId = "HD001";
            else
            {
                int num = int.Parse(lastId.Substring(2)) + 1;
                invoiceId = "HD" + num.ToString("D3");
            }


            string customerId = "KH001";

            decimal totalAmount = Convert.ToDecimal(amount);

            var invoiceRes = ApiClient.Client.Send(new
            {
                action = "create_invoice",
                data = new
                {
                    invoiceId,
                    customerId,
                    totalAmount
                }
            });

            if (invoiceRes == null || invoiceRes.status != "success")
            {
                MessageBox.Show("Tạo hóa đơn thất bại!");
                return;
            }

            var maxDetail = ApiClient.Client.Send(new { action = "get_max_invoice_detail_id" });

            string detailId;
            if (maxDetail?.maxId == null || maxDetail.maxId == "")
                detailId = "CTHD001";
            else
            {
                int num = int.Parse(maxDetail.maxId.ToString().Substring(4)) + 1;
                detailId = "CTHD" + num.ToString("D3");
            }

            string serviceId = "2";
            string note = "Nạp tiền";


            ApiClient.Client.Send(new
            {
                action = "create_invoice_detail_top_up",
                data = new
                {
                    detailId,
                    invoiceId,
                    serviceId,
                    quantity = 1,
                    totalAmount,
                    note
                }
            });

            MessageBox.Show("Đã yêu cầu nạp tiền thành công!");
            this.Close();
        }
    }
}
