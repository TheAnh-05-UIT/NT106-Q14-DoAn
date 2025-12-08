using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.Uc_Staff
{
    public partial class uc_Staff_Notification_Item : UserControl
    {
        public uc_Staff_Notification_Item(string title, string a, string b, string c, Action<object,EventArgs> meth = null, Action<object, EventArgs> rmv = null)
        {
            InitializeComponent();
            label1.Text = title;
            label3.Text = a;
            label2.Text = b;
            button1.Text = c;
            if (meth != null)
            {
                try {
                    button1.Click += (s, e) => meth(s, e); 
                }
                catch
                {}
            }
            button2.Click += (s, e) => rmv(s, e);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
