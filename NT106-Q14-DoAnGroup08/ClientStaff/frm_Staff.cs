using Guna.UI2.WinForms;
using NT106_Q14_DoAnGroup08.Uc_Staff;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class frm_Staff : Form
    {
        private Uc_Staff.uc_Staff_ImportGood ImportGood;
        private Uc_Staff.uc_Staff_Menu Menu;
        private Uc_Staff.uc_Staff_Bills Bills;
        private Uc_Staff.uc_Staff_Chat Chat;
        private Uc_Staff.uc_Staff_Account Account;
        private Uc_Staff.uc_Staff_Notification Notification = new Uc_Staff.uc_Staff_Notification();

        private TcpClient notifyClient;
        private Thread notifyThread;
        private volatile bool notifyRunning;
        private readonly string notifyServerIp = "127.0.0.1";
        private readonly int notifyServerPort = 8080;
        private readonly string notifyClientName = "Staff";

        public frm_Staff()
        {
            InitializeComponent();
            ImportGood = new Uc_Staff.uc_Staff_ImportGood();
            Menu = new Uc_Staff.uc_Staff_Menu();
            Bills = new Uc_Staff.uc_Staff_Bills();
            Chat = new Uc_Staff.uc_Staff_Chat();
            Account = new Uc_Staff.uc_Staff_Account();
            Notification = new Uc_Staff.uc_Staff_Notification();
            updateNotificationCount();

            StartNotificationClient();

            this.FormClosing += Frm_Staff_FormClosing;
        }

        private void updateNotificationCount()
        {
            groupBox1.Text = Notification.GetAllItems().Count().ToString();
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
            ShowUserControl(Chat, sender);
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

        public void receiveNotification(string title, string content, string btnContent, string time = null)
        {
            Notification.createItem(title, content, btnContent, time, updateNotificationCount);
            updateNotificationCount();
            Control focusedControl = this.ActiveControl;
            if (focusedControl?.GetType().ToString() != "NT106_Q14_DoAnGroup08.Uc_Staff.uc_Staff_Notification")
            {
                SystemSounds.Asterisk.Play();
                groupBox1.BackColor = Color.Red;
                groupBox1.ForeColor = Color.White;
            }
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
            if (notifyThread != null && notifyThread.IsAlive) return;
            notifyRunning = true;
            notifyThread = new Thread(NotificationWorker) { IsBackground = true };
            notifyThread.Start();
        }

        private void StopNotificationClient()
        {
            notifyRunning = false;
            try
            {
                try { notifyClient?.Close(); } catch { }
            }
            catch { }
            try
            {
                if (notifyThread != null && notifyThread.IsAlive)
                {
                    if (!notifyThread.Join(2000))
                        notifyThread.Abort();
                }
            }
            catch { }
        }

        private void NotificationWorker()
        {
            while (notifyRunning)
            {
                try
                {
                    notifyClient = new TcpClient();
                    notifyClient.Connect(notifyServerIp, notifyServerPort);

                    using (var ns = notifyClient.GetStream())
                    {
                        byte[] nameBytes = Encoding.UTF8.GetBytes(notifyClientName);
                        ns.Write(nameBytes, 0, nameBytes.Length);

                        byte[] buffer = new byte[8192];
                        while (notifyRunning && notifyClient.Connected)
                        {
                            int bytesRead = 0;
                            try
                            {
                                bytesRead = ns.Read(buffer, 0, buffer.Length);
                                if (bytesRead == 0) break;
                            }
                            catch (Exception)
                            {
                                break;
                            }

                            string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                            if (string.IsNullOrEmpty(msg)) continue;

                            if (msg.StartsWith("NOTIFICATION|"))
                            {
                                string payload = msg.Substring("NOTIFICATION|".Length);
                                try
                                {
                                    dynamic notif = JsonConvert.DeserializeObject(payload);
                                    string title = notif.title != null ? notif.title.ToString() : "Thông báo";
                                    string content = notif.content != null ? notif.content.ToString() : "";
                                    string time = notif.time != null ? notif.time.ToString() : null;

                                    if (!this.IsDisposed && !this.Disposing)
                                    {
                                        try
                                        {
                                            this.BeginInvoke(new Action(() =>
                                            {
                                                receiveNotification(title, content, "OK", time);
                                            }));
                                        }
                                        catch {}
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine("Failed parse notification payload: " + ex.Message);
                                }
                            }
                            else
                            {
                                Debug.WriteLine("Notification client received: " + msg);
                            }
                        }
                    }
                }
                catch (SocketException ex)
                {
                    Debug.WriteLine("Notification socket error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Notification client error: " + ex.Message);
                }
                finally
                {
                    try { notifyClient?.Close(); } catch { }
                }

                if (notifyRunning)
                {
                    Thread.Sleep(3000);
                }
            }
        }

        private void Frm_Staff_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopNotificationClient();
        }
    }
}
