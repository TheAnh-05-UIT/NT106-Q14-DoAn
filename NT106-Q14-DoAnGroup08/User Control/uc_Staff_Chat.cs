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
    public partial class uc_Staff_Chat : UserControl
    {
        public uc_Staff_Chat()
        {
            InitializeComponent();
            this.Load += uc_Staff_Chat_Load;
        }

        private void uc_Staff_Chat_Load(object sender, EventArgs e)
        {
            // Wire events
            btn_SendMessage.Click += Btn_SendMessage_Click;
            txt_Chat.KeyDown += Txt_Chat_KeyDown;

            // UI tweaks
            lst_Chat.HorizontalScrollbar = true;
            txt_Chat.Focus();

            // Load history if available (placeholder)
            LoadChatHistory();
        }

        private void LoadChatHistory()
        {
            // Placeholder: load chat history from DB if you implement chat persistence.
            // Keep empty for now so UI shows nothing until user sends messages.
        }

        private void Txt_Chat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent ding
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

            // In this simple UI-only implementation we show messages with timestamp and a simple sender label.
            string senderName = "Nhân viên"; // you can replace with current user name variable
            string entry = $"[{DateTime.Now:dd/MM/yyyy HH:mm}] {senderName}: {text}";

            lst_Chat.Items.Add(entry);
            lst_Chat.TopIndex = lst_Chat.Items.Count - 1; // autoscroll

            txt_Chat.Clear();
            txt_Chat.Focus();
        }
    }
}
