using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.Uc_Staff
{
    public partial class uc_Staff_Notification : UserControl
    {
        List<Control> myControls = new List<Control>();
        public uc_Staff_Notification()
        {
            InitializeComponent();
        }

        public void createItem(string title, string content, string buttonContent, string Time = null, Action additionalMethod = null, Action addRmvBtn = null)
        {
            if (Time == null)
            {
                Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }

            uc_Staff_Notification_Item thisItem = null;

            Action<object, EventArgs> defaultRemove = (s, e) =>
            {
                if (myControls.Contains(thisItem))
                {
                    myControls.Remove(thisItem);
                    additionalMethod?.Invoke();
                    thisItem.Dispose();
                    Render();
                }
            };

            Action<object, EventArgs> buttonRemove = (s, e) =>
            {
                if (myControls.Contains(thisItem))
                {
                    myControls.Remove(thisItem);
                    addRmvBtn?.Invoke();
                    thisItem.Dispose();
                    Render();
                }
            };

            thisItem = new uc_Staff_Notification_Item(title, content, Time, buttonContent, defaultRemove, buttonRemove);

            myControls.Insert(0, thisItem);
            Render();
        }

        public List<Control> GetAllItems()
        {
            return myControls;
        }

        public async void Render()
        {
            flowLayoutPanel1.SuspendLayout();
            this.Focus();
            flowLayoutPanel1.Controls.Clear();
            this.Focus();
            int availableWidth = flowLayoutPanel1.ClientSize.Width - flowLayoutPanel1.Padding.Horizontal;
            foreach (Control myControl in myControls)
            {
                flowLayoutPanel1.Controls.Add(myControl);
                myControl.Width = availableWidth - myControl.Margin.Horizontal;
            }
            flowLayoutPanel1.ResumeLayout();
        }

        private void uc_Staff_Notification_Load(object sender, EventArgs e)
        {
            Render();
        }

        private void uc_Staff_Notification_VisibleChanged(object sender, EventArgs e)
        {
            Render();
        }

        private void uc_Staff_Notification_Enter(object sender, EventArgs e)
        {
            Render();
        }

        private void flowLayoutPanel1_Enter(object sender, EventArgs e)
        {

        }
    }
}
