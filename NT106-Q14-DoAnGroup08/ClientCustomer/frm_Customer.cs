using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NT106_Q14_DoAnGroup08.ConnectionServser;
using QuanLyQuanNet.DTOs;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    public partial class frm_Customer : Form
    {
        private string _userId;
        private string _sessionId;

        private int _timeLeft;
        private int _timeUsed;
        private decimal _moneyLeft;
        private decimal _moneyUsed;

        private Timer sessionTimer;

        private int _initialTimeLeft;

        private bool notified15 = false;
        private bool notified5 = false;
        private bool notified3 = false;
        private bool notified2 = false;
        private bool notified1 = false;

        public frm_Customer(string userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int formWidth = this.Width;

            this.Location = new Point(screenWidth - formWidth + 10);

            StartSessionFromServer();
            StartUpdateTimer();
        }

        private void StartSessionFromServer()
        {
            var req = new
            {
                action = "start_session",
                customerId = _userId,
                computerId = Environment.MachineName
            };

            string json = JsonConvert.SerializeObject(req);
            string res = ServerConnection.SendRequest(json);

            // Debug: hiển thị phản hồi server
            if (string.IsNullOrWhiteSpace(res))
            {
                MessageBox.Show("Không nhận được phản hồi từ server!\nKiểm tra kết nối hoặc Server đang tắt.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Uncomment dòng dưới để xem phản hồi JSON từ Server
            // else
            // {
            //     MessageBox.Show("Server trả về:\n" + res, "Debug - StartSession", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // }

            try
            {
                var obj = JsonConvert.DeserializeObject<StartSessionResponse>(res);

                if (obj == null)
                {
                    MessageBox.Show("Không thể parse phản hồi từ server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (obj.status == "success")
                {
                    _sessionId = obj.sessionId;
                    _timeLeft = obj.timeLeft;
                    _timeUsed = obj.timeUsed;
                    _moneyLeft = obj.moneyLeft;
                    _moneyUsed = obj.moneyUsed;
                    _initialTimeLeft = _timeLeft;

                    notified15 = false;
                    notified5 = false;
                    notified3 = false;
                    notified2 = false;
                    notified1 = false;

                    lbl_CName.Text = obj.computerName;

                    txt_TimeUsed.Text = FormatTime(_timeUsed);
                    txt_TimeRemain.Text = FormatTime(_timeLeft);
                    txt_BalanceUsed.Text = _moneyUsed.ToString("N0") + " đ";
                    txt_BalanceRemain.Text = _moneyLeft.ToString("N0") + " đ";


                    lbl_Username.Text = obj.userName;
                }
                else
                {
                    // Hiển thị thông điệp lỗi chi tiết từ server
                    string msg = obj.message ?? "Không thể bắt đầu phiên!";
                    MessageBox.Show($"Lỗi server: {msg}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (JsonReaderException ex)
            {
                MessageBox.Show($"Phản hồi không hợp lệ từ server! Vui lòng kiểm tra lại cấu trúc JSON. Chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Client: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Convert giây → "HH:mm:ss"
        private string FormatTime(int seconds)
        {
            if (seconds < 0) seconds = 0;
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return $"{(int)t.TotalHours:00}:{t.Minutes:00}";

        }

        private void StartUpdateTimer()
        {
            sessionTimer = new Timer();
            sessionTimer.Interval = 60000; // 1 phút
            sessionTimer.Tick += SessionTimer_Tick;
            sessionTimer.Start();
        }

        private void CheckLowTimeWarning()
        {
            if (_initialTimeLeft > 900)
            {
                if (_timeLeft <= 900 && !notified15)
                {
                    notified15 = true;
                    MessageBox.Show("Bạn còn 15 phút sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (_initialTimeLeft > 300 && _initialTimeLeft < 899)
            {
                if (_timeLeft <= 300 && !notified5)
                {
                    notified5 = true;
                    MessageBox.Show("Bạn còn 5 phút sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (_timeLeft <= 180 && !notified3)  // 3 phút
            {
                notified3 = true;
                MessageBox.Show("Bạn còn 3 phút sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (_timeLeft <= 120 && !notified2)  // 2 phút
            {
                notified2 = true;
                MessageBox.Show("Bạn còn 2 phút sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (_timeLeft <= 60 && !notified1)  // 1 phút
            {
                notified1 = true;
                MessageBox.Show("Bạn còn 1 phút sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_sessionId)) return;

            var req = new
            {
                action = "update_session",
                sessionId = _sessionId,
            };

            string res = ServerConnection.SendRequest(JsonConvert.SerializeObject(req));
            if (string.IsNullOrEmpty(res)) return;

            dynamic obj = JsonConvert.DeserializeObject(res);
            if (obj.status == "success" || obj.status == "ended")
            {
                // Cập nhật biến từ Server
                _timeUsed = obj.timeUsed;
                _timeLeft = obj.timeLeft;
                _moneyUsed = Convert.ToDecimal(obj.moneyUsed);
                _moneyLeft = Convert.ToDecimal(obj.moneyLeft);

                // Cập nhật UI theo layout mong muốn
                txt_TimeUsed.Text = FormatTime(_timeUsed);
                txt_TimeRemain.Text = FormatTime(_timeLeft);
                txt_BalanceUsed.Text = _moneyUsed.ToString("N0") + " đ";
                txt_BalanceRemain.Text = _moneyLeft.ToString("N0") + " đ";

                CheckLowTimeWarning();

                if (obj.status == "ended")
                {
                    sessionTimer.Stop();
                    MessageBox.Show("Phiên đã kết thúc do hết tiền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void EndSessionToServer()
        {
            var req = new
            {
                action = "end_session",
                sessionId = _sessionId
            };

            string json = JsonConvert.SerializeObject(req);
            ServerConnection.SendRequest(json);
        }
        private void btn_Chat_Click(object sender, EventArgs e)
        {
            frm_Customer_Chat f = new frm_Customer_Chat(_userId);
            f.Show();
        }

        private void btn_FoodMenu_Click(object sender, EventArgs e)
        {
            frm_Customer_FoodMenu f = new frm_Customer_FoodMenu(_userId);
            f.TopMost = true;
            f.WindowState = FormWindowState.Normal;
            f.Show();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            sessionTimer.Stop();
            EndSessionToServer();
            this.Close();
        }

        private void btn_TopUp_Click(object sender, EventArgs e)
        {
            frm_Customer_TopUp f = new frm_Customer_TopUp();
            f.Show();
        }

        public class StartSessionResponse
        {
            public string status { get; set; }
            public string message { get; set; }

            public string sessionId { get; set; }
            public int timeLeft { get; set; }
            public int timeUsed { get; set; }
            public decimal moneyLeft { get; set; }
            public decimal moneyUsed { get; set; }
            public string computerName { get; set; }

            public string customerId { get; set; }
            public string userName { get; set; }
        }
    }
}