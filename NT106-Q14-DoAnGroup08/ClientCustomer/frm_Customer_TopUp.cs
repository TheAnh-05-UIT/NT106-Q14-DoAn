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
    public partial class frm_Customer_TopUp : Form
    {
        public frm_Customer_TopUp()
        {
            InitializeComponent();
        }

        private void btn_TopUp_5000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = btn_TopUp_5000.Text;
        }

        private void btn_TopUp_10000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = btn_TopUp_10000.Text;
        }

        private void btn_TopUp_20000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = btn_TopUp_20000.Text;
        }

        private void btn_TopUp_30000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = btn_TopUp_30000.Text;
        }

        private void btn_TopUp_50000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = btn_TopUp_50000.Text;
        }

        private void btn_TopUp_100000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = btn_TopUp_100000.Text;
        }

        private void btn_TopUp_200000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = btn_TopUp_200000.Text;
        }

        private void btn_TopUp_500000_Click(object sender, EventArgs e)
        {
            txt_TopUp.Text = btn_TopUp_500000.Text;
        }
    }
}
