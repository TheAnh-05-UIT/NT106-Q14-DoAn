using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.ClientAdmin
{
    public partial class frm_Revenue : Form
    {
        public frm_Revenue()
        {
            InitializeComponent();
        }

        private void frm_Revenue_Load(object sender, EventArgs e)
        {
            cboMode.SelectedIndex = 0;
        }

        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {

        }
    }
}
