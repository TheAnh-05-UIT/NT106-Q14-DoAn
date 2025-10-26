using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design.WebControls;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace NT106_Q14_DoAnGroup08
{
    public partial class frm_Staff : Form
    {
        private uc_Staff_ImportGood ImportGood;
        private uc_Staff_Menu Menu;
        private uc_Staff_Bills Bills;
        private uc_Staff_Chat Chat;
        private uc_Staff_Account Account;
        public frm_Staff()
        {
            InitializeComponent();
            ImportGood = new uc_Staff_ImportGood();
            Menu = new uc_Staff_Menu();
            Bills = new uc_Staff_Bills();
            Chat = new uc_Staff_Chat();
            Account = new uc_Staff_Account();
            ShowUserControl(ImportGood);
        }

        private void ShowUserControl(UserControl newControl)
        {
            UserPanel.Controls.Clear();

            newControl.Dock = DockStyle.Fill;

            UserPanel.Controls.Add(newControl);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Staff_Load(object sender, EventArgs e)
        {

        }

        private void ImportGoodButton_Click(object sender, EventArgs e)
        {
            ShowUserControl(ImportGood);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowUserControl(Menu);
        }

        private void btnQuanLyMay_Click(object sender, EventArgs e)
        {
            frm_Staff_ComputerManagement f = new frm_Staff_ComputerManagement();
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            UserPanel.Controls.Clear();
            UserPanel.Controls.Add(f);
            f.Show();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            ShowUserControl(Bills);
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            ShowUserControl(Chat);
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            ShowUserControl(Account);
        }
    }
}
