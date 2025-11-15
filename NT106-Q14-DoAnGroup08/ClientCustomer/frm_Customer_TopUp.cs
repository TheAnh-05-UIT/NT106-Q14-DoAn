using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using QRCoder;
using System.Net;

namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    public partial class frm_Customer_TopUp : Form
    {
        public frm_Customer_TopUp()
        {
            InitializeComponent();
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
            int acqId = 970436; long accountNo = 1036339042; 
            string accountName = "TRAN MINH HOANG QUAN"; 
            int Amount = int.Parse(txt_TopUp.Text); 
            ApiRequest apiRequest = new ApiRequest() 
            {   acqId = acqId, 
                accountNo = accountNo, 
                accountName = accountName, 
                amount = Amount, addInfo = $"NAPTIEN_{Amount}", 
                format = "text", template = "compact" 
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
            frm_Customer_QRCode f = new frm_Customer_QRCode(base64QR); 
            f.ShowDialog(); 
        }
    }
}
