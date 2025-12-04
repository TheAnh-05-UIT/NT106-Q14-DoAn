using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    public partial class frm_Customer_Chat : Form
    {
        private string customerId;
        private string staffId;
        private Timer chatTimer;

        public frm_Customer_Chat(string customerId)
        {
            InitializeComponent();
            this.customerId = customerId;
        }

        private void frm_Customer_Chat_Load(object sender, EventArgs e)
        {
            btn_Send.Click += Btn_Send_Click;
            txt_Chat.KeyDown += Txt_Chat_KeyDown;

            // Timer polling tin nhắn
            chatTimer = new Timer();
            chatTimer.Interval = 1500; // 1.5s
            chatTimer.Tick += ChatTimer_Tick;
            chatTimer.Start();
        }

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void Txt_Chat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendMessage();
            }
        }

        private void SendMessage()
        {
            string text = txt_Chat.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;

            lst_Chat.Items.Add($"[{DateTime.Now:HH:mm}] Tôi: {text}");
            lst_Chat.TopIndex = lst_Chat.Items.Count - 1;

            var res = ApiClient.Client.Send(new
            {
                action = "send_message_customer",
                from = customerId,
                content = txt_Chat.Text
            });

            if (res?.status == "error")
            {
                MessageBox.Show(res.message.ToString(), "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (staffId == null && res?.to != null)
            {
                staffId = res.to.ToString(); // Lưu nhân viên đang chat
            }

            txt_Chat.Clear();
            txt_Chat.Focus();
        }


        private void ChatTimer_Tick(object sender, EventArgs e)
        {
            var res = ApiClient.Client.Send(new
            {
                action = "get_unread_messages_customer",
                userId = customerId
            });

            if (res == null) return;

            foreach (var msg in res)
            {
                string content = msg.Content.ToString();
                lst_Chat.Items.Add($"[{DateTime.Now:HH:mm}] NV: {content}");
                lst_Chat.TopIndex = lst_Chat.Items.Count - 1;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            chatTimer?.Stop();
            chatTimer?.Dispose();
            base.OnFormClosed(e);
        }

    }
}
