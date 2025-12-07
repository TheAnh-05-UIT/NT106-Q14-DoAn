using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace NT106_Q14_DoAnGroup08.Uc_Staff
{
    public partial class uc_Staff_Chat : UserControl
    {
        string fromId; // nhân viên
        string toId;   // khách hàng
        private int willBeDeleted = -1;

        public event Action<string, string, string> OnSendMessage;


        public uc_Staff_Chat()
        {
            InitializeComponent();
            this.Load += uc_Staff_Chat_Load;
            lst_Chat.MouseDown += actionStopNoti;
            txt_Chat.MouseDown += actionStopNoti;
            btn_SendMessage.MouseDown += actionStopNoti;
        }

        public void actionStopNoti(object s, EventArgs e)
        {
            lst_Chat.ForeColor = Color.Black;
            if (willBeDeleted != -1)
                lst_Chat.Items.RemoveAt(willBeDeleted);
            willBeDeleted = -1;
        }

        private void uc_Staff_Chat_Load(object sender, EventArgs e)
        {
            btn_SendMessage.Click += Btn_SendMessage_Click;
            txt_Chat.KeyDown += Txt_Chat_KeyDown;

            lst_Chat.HorizontalScrollbar = true;
            txt_Chat.Focus();

            LoadChatHistory();
        }

        private void LoadChatHistory()
        {

        }

        private void Txt_Chat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendMessageFromCurrentUser();
            }
        }

        private void Btn_SendMessage_Click(object sender, EventArgs e)
        {
            SendMessageFromCurrentUser();
        }

        private void SendMessageFromCurrentUser()
        {
            string text = txt_Chat.Text?.Trim();
            if (string.IsNullOrEmpty(text)) return;

            string entry = $"[{DateTime.Now:dd/MM/yyyy HH:mm}] Tôi: {text}";
            lst_Chat.Items.Add(entry);
            lst_Chat.TopIndex = lst_Chat.Items.Count - 1;

            OnSendMessage?.Invoke(fromId, toId, text);

            txt_Chat.Clear();
            txt_Chat.Focus();
        }

        public void AddMessage(string msg)
        {
            string entry = $"[{DateTime.Now:dd/MM/yyyy HH:mm}] {msg}";
            if (willBeDeleted == -1)
            {
                lst_Chat.Items.Add("New message received:");
                willBeDeleted = lst_Chat.Items.Count - 1;
            }
            lst_Chat.Items.Add(entry);
            lst_Chat.ForeColor = Color.Red;
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
