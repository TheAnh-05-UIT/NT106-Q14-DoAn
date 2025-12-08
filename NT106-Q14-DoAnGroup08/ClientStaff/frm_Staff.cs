using Guna.UI2.WinForms;
using NT106_Q14_DoAnGroup08.Uc_Staff;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using NT106_Q14_DoAnGroup08.ConnectionServser;
using QuanLyQuanNet.Utils;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.Design.WebControls;
using System.Windows.Forms;
using System.Text.Json.Serialization;

namespace NT106_Q14_DoAnGroup08.ClientStaff
{
    // Tại sao cần timer trong form staff
    public partial class frm_Staff : Form
    {
        private Uc_Staff.uc_Staff_ImportGood ImportGood;
        private readonly Uc_Staff.uc_Staff_Menu Menu;
        private Uc_Staff.uc_Staff_Bills Bills;
        private Uc_Staff.uc_Staff_Account Account;
        private Uc_Staff.uc_Staff_Notification Notification;
        private TabControl tabChat;
        private Dictionary<string, Uc_Staff.uc_Staff_Chat> chatTabs
    = new Dictionary<string, Uc_Staff.uc_Staff_Chat>();
        private System.Windows.Forms.Timer chatTimer;

        private string staffId = "Staff";

        public frm_Staff(string staffId)
        {
            InitializeComponent();
            this.staffId = staffId;

            ImportGood = new Uc_Staff.uc_Staff_ImportGood();
            Menu = new Uc_Staff.uc_Staff_Menu();
            Bills = new Uc_Staff.uc_Staff_Bills();
            Account = new Uc_Staff.uc_Staff_Account();
            Notification = new Uc_Staff.uc_Staff_Notification();

            updateNotificationCount();

            Account.SetStaffId(staffId);
            Account.LogoutClicked += Account_LogoutClicked;

            StartNotificationClient();
            ShowUserControl(Account, btnTaiKhoan);
            this.FormClosing += Frm_Staff_FormClosing;
            StartChatPolling();
            this.Show();
        }

        private void refreshButton(object sender)
        {
            foreach (var button in ButtonGroup.Controls)
            {
                if (button is Button)
                {
                    if (button == sender)
                    {
                        ((Button)button).BackColor = Color.IndianRed;
                        ((Button)button).ForeColor = Color.White;
                    }
                    else
                    {
                        ((Button)button).BackColor = Color.LightSalmon;
                        ((Button)button).ForeColor = SystemColors.ControlText;
                    }
                }
                else
                {
                    if (button is GroupBox)
                    {
                        foreach (var gbButton in ((GroupBox)button).Controls)
                        {
                            if (gbButton is Button)
                            {
                                if (gbButton == sender)
                                {
                                    ((Button)gbButton).BackColor = Color.IndianRed;
                                    ((Button)gbButton).ForeColor = Color.White;
                                }
                                else
                                {
                                    ((Button)gbButton).BackColor = Color.LightSalmon;
                                    ((Button)gbButton).ForeColor = SystemColors.ControlText;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void updateNotificationCount()
        {
            groupBox1.Text = Notification.GetAllItems().Count.ToString();
        }


        private void ShowUserControl(UserControl newControl, object sender)
        {
            UserPanel.Controls.Clear();
            newControl.Dock = DockStyle.Fill;
            UserPanel.Controls.Add(newControl);
            refreshButton(sender);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Staff_Load(object sender, EventArgs e)
        {
        }

        private void ImportGoodButton_Click(object sender, EventArgs e)
        {
            ShowUserControl(ImportGood, sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowUserControl(Menu, sender);
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
            refreshButton(sender);
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            ShowUserControl(Bills, sender);
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            ShowChatPanel();
            refreshButton(sender);
        }

        private void ShowChatPanel()
        {
            UserPanel.Controls.Clear();

            if (tabChat == null)
            {
                tabChat = new TabControl();
                this.GotFocus += (s, e) => { InvokeGotFocus(tabChat, e); };
                tabChat.Dock = DockStyle.Fill;
            }

            tabChat.Parent = UserPanel;
            UserPanel.Controls.Add(tabChat);
            tabChat.BringToFront();
        }

        public void OpenChatTab(string customerId)
        {
            ShowChatPanel();
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;

            if (chatTabs.ContainsKey(customerId))
            {
                tabChat.SelectedTab = chatTabs[customerId].Parent as TabPage;
                return;
            }

            TabPage tab = new TabPage(customerId);

            var chat = new Uc_Staff.uc_Staff_Chat();
            chat.Dock = DockStyle.Fill;
            chat.SetUser(staffId, customerId);

            // Gắn sự kiện gửi tin lên server
            chat.OnSendMessage += SendMessageToServer;

            tab.Controls.Add(chat);
            tabChat.TabPages.Add(tab);

            chatTabs[customerId] = chat;
            tabChat.SelectedTab = tab;
        }

        private void SendMessageToServer(string from, string to, string content)
        {
            ApiClient.Client.Send(new
            {
                action = "send_message_staff",
                from = from,
                to = to,
                content = content
            });
        }

        public void OnReceiveMessage(string customerId, string content)
        {
            OpenChatTab(customerId);
            chatTabs[customerId].AddMessage($"{customerId}: {content}");
        }

        private void StartChatPolling()
        {
            chatTimer = new System.Windows.Forms.Timer();
            chatTimer.Interval = 1500; // 1.5 giây
            chatTimer.Tick += ChatTimer_Tick;
            chatTimer.Start();
        }

        private void ChatTimer_Tick(object sender, EventArgs e)
        {
            var res = ApiClient.Client.Send(new
            {
                action = "get_unread_messages_staff",
                staffId = staffId
            });

            if (res == null || res.Count == 0) return;

            foreach (var msg in res)
            {
                if (msg == null) continue;
                if (msg.FromId == null || msg.Content == null) continue;

                string customerId = msg.FromId.ToString();
                string content = msg.Content.ToString();

                OnReceiveMessage(customerId, content);
                receiveNotification("Tin nhắn mới", $"Tin nhắn từ {customerId}\n{content}", "Xem ngay", null, () => { OpenChatTab(customerId); });
    }
        }


        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            ShowUserControl(Account, sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowUserControl(Notification, sender);
            groupBox1.BackColor = Color.White;
            Notification.Render();
            Notification.Focus();
        }

        public void receiveNotification(string title, string content, string btnContent, string time = null, Action additionalMethod = null)
        {
            if (additionalMethod != null) {
                additionalMethod += updateNotificationCount;
            }
            else
            {
                additionalMethod = updateNotificationCount;
            }
            Notification.createItem(title, content, btnContent, time, additionalMethod, updateNotificationCount);
            updateNotificationCount();
            Control focusedControl = this.ActiveControl;
            if (focusedControl?.GetType().ToString() != "NT106_Q14_DoAnGroup08.Uc_Staff.uc_Staff_Notification")
            {
                SystemSounds.Asterisk.Play();
                groupBox1.BackColor = Color.Red;
                groupBox1.ForeColor = Color.White;
            }
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void UserPanel_Enter(object sender, EventArgs e)
        {
            Control focusedControl = this.ActiveControl;
            if (focusedControl?.GetType().ToString() == "NT106_Q14_DoAnGroup08.Uc_Staff.uc_Staff_Notification")
            {
                groupBox1.BackColor = Color.White;
                groupBox1.ForeColor = Color.Black;
            }
        }

        private void frm_Staff_Activated(object sender, EventArgs e)
        {
        }

        private void StartNotificationClient()
        {
            // Subscribe to NotifyClient events and start it
            try
            {
                NotifyClient.Instance.NotificationReceived += NotifyClient_NotificationReceived;
                NotifyClient.Instance.MessageReceived += NotifyClient_MessageReceived;
                NotifyClient.Instance.Start(staffId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("StartNotificationClient error: " + ex.Message);
            }
        }

        private void StopNotificationClient()
        {
            try
            {
                NotifyClient.Instance.NotificationReceived -= NotifyClient_NotificationReceived;
                NotifyClient.Instance.MessageReceived -= NotifyClient_MessageReceived;
                NotifyClient.Instance.Stop();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("StopNotificationClient error: " + ex.Message);
            }
        }

        private void NotifyClient_NotificationReceived(dynamic notif)
        {
            if (!this.IsDisposed && !this.Disposing)
            {
                try
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        string title = notif.title != null ? notif.title.ToString() : "Thông báo";
                        string content = notif.content != null ? notif.content.ToString() : "";
                        string time = notif.time != null ? notif.time.ToString() : null;

                        receiveNotification(title, content, "OK", time);
                    }));
                }
                catch { }
            }
        }

        private void NotifyClient_MessageReceived(string from, string content)
        {
            if (!this.IsDisposed && !this.Disposing)
            {
                try
                {
                    this.BeginInvoke(new Action(() => OnReceiveMessage(from, content)));
                }
                catch { }
            }
        }

        private void Frm_Staff_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopNotificationClient();
        }

        private void Account_LogoutClicked(object sender, EventArgs e)
        {
            try
            {
                chatTimer?.Stop(); 
            }
            catch { }

            this.Close();         
            new frm_Login().Show(); 
        }

    }
}
