using NT106_Q14_DoAnGroup08.ClientCustomer;
using NT106_Q14_DoAnGroup08.DTO; // Thêm dòng này để truy cập DTO.UserSession
using NT106_Q14_DoAnGroup08.ConnectionServser;
using QuanLyQuanNet.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace NT106_Q14_DoAnGroup08.ClientAdmin
{
    public partial class frm_Account_Admin : Form
    {
        private string currentUserId;
        private string currentUserName;
       
        public frm_Account_Admin()
        {
            InitializeComponent();
            
        }

        private void frm_Account_Admin_Load(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(currentUserId))
            {
                lblUserNameVer2.Text = currentUserId;
                lblFullNameVer2.Text = currentUserName;
               
            }
       
            else
            {
                lblUserNameVer2.Text = "(Lỗi tải thông tin)";
                lblFullNameVer2.Text = "";
            }
        }
        public void changeLblTitle(string uname, string fullname)
        {
            currentUserId = uname;
            currentUserName = fullname;
            lblUserNameVer2.Text = uname;
            lblFullNameVer2.Text = fullname;
        }
    }

}