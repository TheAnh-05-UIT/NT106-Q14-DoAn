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

        public frm_Customer_QRCode(string base64)
        {
            InitializeComponent();
            base64QR = base64;
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
    }
}
