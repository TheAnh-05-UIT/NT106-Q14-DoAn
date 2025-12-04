using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Suite.Descriptions;

namespace NT106_Q14_DoAnGroup08.Uc_Staff
{
    public partial class uc_Staff_Chat : UserControl
    {
        string fromId; // nhân viên
        string toId;   // khách hàng

        public event Action<string, string, string> OnSendMessage;


        public uc_Staff_Chat()
        {
            InitializeComponent();
            createTab("Tổng quát", new uc_Staff_Chat_Overview(createChatWindow), false);
            this.Load += (s, e) =>
            {
                UserPanel.Size = new Size(0, chatTabs[0].Size.Height + 20);
                UserPanel.AutoSize = false;
                Render();
            };
        }

        private void clearNewMessageNotification(string userName)
        {
            foreach (uc_Staff_Chat_Tab tab in chatTabs)
            {
                if (tab.Title == userName)
                {
                    tab.ClearNewMessageNotification();
                    return;
                }
            }
        }

        public void receiveMessage(string userId, string userName, string message)
        {
            createChatWindow(userId, userName);
            foreach (uc_Staff_Chat_Tab tab in chatTabs)
            {
                if (tab.Title == userName)
                {
                    int index = chatTabs.IndexOf(tab);
                    uc_Staff_Chat_Window chatWindow = Windows[index] as uc_Staff_Chat_Window;
                    if (chatWindow != null)
                    {
                        chatWindow.ReceiveMessage(userName, message);
                        notifyNewMessage(userId, userName);
                    }
                    return;
                }
            }
        }

        public void notifyNewMessage(string userId, string userName)
        {
            foreach (uc_Staff_Chat_Tab tab in chatTabs)
            {
                if (tab.Title == userName)
                {
                    tab.NotifyNewMessage();
                    return;
                }
            }
        }

        private void createChatWindow(string userId, string userName)
        {
            foreach (uc_Staff_Chat_Tab tab in chatTabs)
            {
                if (tab.Title == userName)
                {
                    switchActiveTab(tab);
                    ShowWindow(Windows[chatTabs.IndexOf(tab)]);
                    Render();
                    return;
                }
            }
            uc_Staff_Chat_Window chatWindow = new uc_Staff_Chat_Window(userId, ()=>clearNewMessageNotification(userName));
            createTab(userName, chatWindow);
        }

        private void ShowWindow(UserControl newControl)
        {
            panel2.Controls.Clear();
            newControl.Dock = DockStyle.Fill;
            panel2.Controls.Add(newControl);
        }

        private void switchActiveTab(uc_Staff_Chat_Tab activeTab)
        {
            foreach (uc_Staff_Chat_Tab tab in chatTabs)
            {
                if (tab == activeTab)
                {
                    tab.SetActive(true);
                }
                else
                {
                    tab.SetActive(false);
                }
            }
        }

        public void createTab(string title, UserControl uc = null, bool closable = true, bool active = true)
        {
            uc_Staff_Chat_Tab thisItem = null;
            Action<object, EventArgs> defaultRemove;
            Action<object, EventArgs> direct;
            Windows.Add(uc);

            if (!closable)
            {
                defaultRemove = null;
            }
            else
                defaultRemove = (s, e) =>
            {
                if (chatTabs.Contains(thisItem))
                {
                    chatTabs.Remove(thisItem);
                    if (thisItem.IsActive())
                    {
                        switchActiveTab(chatTabs[0]);
                        ShowWindow(Windows[0]);
                    }
                    Windows.Remove(uc);
                    Render();
                }
            };

            direct = (s, e) =>
            {
                if (chatTabs.Contains(thisItem) && !thisItem.IsActive())
                {
                    switchActiveTab(thisItem);
                    if (uc != null)
                        ShowWindow(uc);
                    Render();
                }
            };


            thisItem = new uc_Staff_Chat_Tab(title, direct, defaultRemove);

            chatTabs.Add(thisItem);
            switchActiveTab(thisItem);
            ShowWindow(uc);

            Render();
        }

        public async void Render()
        {
            UserPanel.SuspendLayout();
            this.Focus();
            UserPanel.Controls.Clear();
            this.Focus();
            foreach (Control tab in chatTabs)
            {
                UserPanel.Controls.Add(tab);
            }
            UserPanel.ResumeLayout();
        }

        private void UserPanel_Paint(object sender, PaintEventArgs e)
        {

            string entry = $"[{DateTime.Now:HH:mm}] Tôi: {text}";
            lst_Chat.Items.Add(entry);
            lst_Chat.TopIndex = lst_Chat.Items.Count - 1;

            // Gửi ra ngoài cho Form Staff xử lý server
            OnSendMessage?.Invoke(fromId, toId, text);

            txt_Chat.Clear();
            txt_Chat.Focus();
        }

        public void AddMessage(string msg)
        {
            lst_Chat.Items.Add($"[{DateTime.Now:HH:mm}] {msg}");
            lst_Chat.TopIndex = lst_Chat.Items.Count - 1;
        }


        public void SetUser(string from, string to)
        {
            fromId = from;
            toId = to;
        }


        private void lst_Chat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
