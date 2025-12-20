using Newtonsoft.Json;
using RestSharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    public partial class frm_Customer_TopUp : Form
    {
        string _userid = string.Empty;
        public frm_Customer_TopUp(string userId)
        {
            InitializeComponent(); this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            _userid = userId;
        }

        private void btn_TopUp_5000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = "5000";
        }

        private void btn_TopUp_10000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = "10000";
        }

        private void btn_TopUp_20000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = "20000";
        }

        private void btn_TopUp_30000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = "30000";
        }

        private void btn_TopUp_50000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = "50000";
        }

        private void btn_TopUp_100000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = "100000";
        }

        private void btn_TopUp_200000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = "200000";
        }

        private void btn_TopUp_500000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = "500000";
        }
        private void btn_QR_Click(object sender, EventArgs e)
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

            int acqId = 970436; long accountNo = 1036339042;
            string accountName = "TRAN MINH HOANG QUAN";
            int Amount = int.Parse(txt_TopUp.Text);
            ApiRequest apiRequest = new ApiRequest()
            {
                acqId = acqId,
                accountNo = accountNo,
                accountName = accountName,
                amount = Amount,
                addInfo = invoiceId,
                format = "text",
                template = "compact"
            };
            string jsonRequest = JsonConvert.SerializeObject(apiRequest);
            var client = new RestSharp.RestClient("https://api.vietqr.io/v2/generate");
            var request = new RestRequest("", Method.Post); request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", jsonRequest, RestSharp.ParameterType.RequestBody);
            var response = client.Execute(request); if (!response.IsSuccessful)
            {
                MessageBox.Show("Không thể tạo QR. Vui lòng thử lại!");
                return;
            }
            var dataResult = JsonConvert.DeserializeObject<ApiResponse>(response.Content);
            if (dataResult == null || dataResult.data == null)
            {
                MessageBox.Show("Lỗi API trả về dữ liệu.");
                return;
            }
            string base64QR = dataResult.data.qrDataURL.Replace("data:image/png;base64,", "");
            frm_Customer_QRCode f = new frm_Customer_QRCode(base64QR, Amount, _userid);
            f.ShowDialog();
        }

        private void btn_Cash_Click(object sender, EventArgs e)
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


            string customerId = _userid;

            decimal totalAmount = Convert.ToDecimal(txt_TopUp.Text);

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
