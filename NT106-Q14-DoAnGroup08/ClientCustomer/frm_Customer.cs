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
    public partial class frm_Customer : Form
    {
        public frm_Customer()
        {
            InitializeComponent();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int formWidth = this.Width;

            this.Location = new Point(screenWidth - formWidth + 10);
        }

        private void btn_Chat_Click(object sender, EventArgs e)
        {
            frm_Customer_Chat f = new frm_Customer_Chat();
            f.ShowDialog();
        }

        private void btn_FoodMenu_Click(object sender, EventArgs e)
        {
            frm_Customer_FoodMenu f = new frm_Customer_FoodMenu();
            f.ShowDialog();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_TopUp_Click(object sender, EventArgs e)
        {
            frm_Customer_TopUp f = new frm_Customer_TopUp();
            f.ShowDialog();
        }
    }
}
