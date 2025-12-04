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

namespace NT106_Q14_DoAnGroup08.ClientStaff
{
    public partial class frm_Staff : Form
    {
        private Uc_Staff.uc_Staff_ImportGood ImportGood;
        private Uc_Staff.uc_Staff_Menu Menu;
        private Uc_Staff.uc_Staff_Bills Bills;
        private Uc_Staff.uc_Staff_Account Account;
        private TabControl tabChat;
        private Dictionary<string, Uc_Staff.uc_Staff_Chat> chatTabs
    = new Dictionary<string, Uc_Staff.uc_Staff_Chat>();
        private Timer chatTimer;

        private string staffId;

        public frm_Staff(string staffId)
        {
            InitializeComponent();
            this.staffId = staffId;

            ImportGood = new Uc_Staff.uc_Staff_ImportGood();
            Menu = new Uc_Staff.uc_Staff_Menu();
            Bills = new Uc_Staff.uc_Staff_Bills();
            Account = new Uc_Staff.uc_Staff_Account();

            Account.SetStaffId(staffId);
            Account.LogoutClicked += Account_LogoutClicked;

            ShowUserControl(ImportGood);
            StartChatPolling();
        }

        private void ShowUserControl(UserControl newControl)
        {
            UserPanel.Controls.Clear();
            newControl.Dock = DockStyle.Fill;
            UserPanel.Controls.Add(newControl);
            newControl.BringToFront();
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
            ShowChatPanel();
        }

        private void ShowChatPanel()
        {
            UserPanel.Controls.Clear();

            if (tabChat == null)
            {
                tabChat = new TabControl();
                tabChat.Dock = DockStyle.Fill;
            }

            tabChat.Parent = UserPanel;
            UserPanel.Controls.Add(tabChat);
            tabChat.BringToFront();
        }

        public void OpenChatTab(string customerId)
        {
            ShowChatPanel();

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
            chatTimer = new Timer();
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
    }
        }


        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            ShowUserControl(Account);
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
