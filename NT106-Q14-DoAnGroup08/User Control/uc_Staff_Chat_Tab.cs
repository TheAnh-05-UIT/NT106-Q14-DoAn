using System;
using System.Drawing;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.Uc_Staff
{
    public partial class uc_Staff_Chat_Tab : UserControl
    {
        bool active = false;
        bool isNewMessageNotified = false;

        public string Title { get => label1.Text; }

        public uc_Staff_Chat_Tab(string title, Action<object, EventArgs> direct = null, Action<object, EventArgs> closeMethod = null)
        {
            InitializeComponent();
            label1.Text = title;
            if (direct != null)
            {
                try
                {
                    panel2.Click += (s, e) => { ClearNewMessageNotification(); direct(s, e); };
                    label1.Click += (s, e) => { ClearNewMessageNotification(); direct(s, e); };
                }
                catch
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
                catch
                { }
            }
        }
        public void SetActive(bool isActive)
        {
            active = isActive;
            if (!isNewMessageNotified)
            {
                if (active)
                {
                    this.BackColor = Color.LightBlue;
                }
                else
                {
                    this.BackColor = SystemColors.Control;
                }
            }
        }

        public bool IsActive()
        {
            return active;
        }

        public void ClearNewMessageNotification()
        {
            isNewMessageNotified = false;
            this.BackColor = active ? Color.LightBlue : SystemColors.Control;
        }

        private void uc_Staff_Chat_Tab_MouseHover(object sender, EventArgs e)
        {
            if (!isNewMessageNotified)
                this.BackColor = active ? Color.LightBlue : Color.LightGray;
        }

        private void uc_Staff_Chat_Tab_MouseLeave(object sender, EventArgs e)
        {
            if (!isNewMessageNotified)
                this.BackColor = active ? Color.LightBlue : SystemColors.Control;
        }

        public void NotifyNewMessage()
        {
            this.BackColor = Color.OrangeRed;
            isNewMessageNotified = true;
        }
    }
}