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
    public partial class uc_Staff_Chat_Window : UserControl
    {
        public uc_Staff_Chat_Window(string UserId)
        {
            InitializeComponent();
            this.Load += uc_Staff_Chat_Load;
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

            string senderName = "Nhân viên";
            string entry = $"[{DateTime.Now:dd/MM/yyyy HH:mm}] {senderName}: {text}";

            lst_Chat.Items.Add(entry);
            lst_Chat.TopIndex = lst_Chat.Items.Count - 1;

            txt_Chat.Clear();
            txt_Chat.Focus();
        }
    }
}
