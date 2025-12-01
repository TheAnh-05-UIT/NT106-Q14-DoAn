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
    public partial class uc_Staff_Chat_Tab : UserControl
    {
        bool active = false;

        public string Title { get => label1.Text; }

        public uc_Staff_Chat_Tab(string title, Action<object, EventArgs> direct = null, Action<object, EventArgs> closeMethod = null)
        {
            InitializeComponent();
            label1.Text = title;
            if (direct != null)
            {
                try
                {
                    panel2.Click += (s, e) => direct(s, e);
                    label1.Click += (s, e) => direct(s, e);
                }
                catch (Exception ex)
                { }
            }
            if (closeMethod != null)
            {
                try
                {
                    button1.Click += (s, e) => closeMethod(s, e);
                    button1.Visible = true;
                    button1.Enabled = true;
                }
                catch (Exception ex)
                { }
            }
        }
        public void SetActive(bool isActive)
        {
            active = isActive;
            if (active)
            {
                this.BackColor = Color.LightBlue;
            }
            else
            {
                this.BackColor = SystemColors.Control;
            }
        }

        public bool IsActive()
        {
            return active;
        }

        private void uc_Staff_Chat_Tab_MouseHover(object sender, EventArgs e)
        {
            this.BackColor = active ? Color.LightBlue : Color.LightGray;
        }

        private void uc_Staff_Chat_Tab_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = active ? Color.LightBlue : SystemColors.Control;
        }
    }
}