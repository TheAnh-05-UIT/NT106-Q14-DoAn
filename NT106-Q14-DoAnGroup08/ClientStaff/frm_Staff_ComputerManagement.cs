using Guna.UI2.WinForms;
using NT106_Q14_DoAnGroup08.ClientCustomer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcpServer;
using TcpServer.Handlers;
using Timer = System.Threading.Timer;

namespace NT106_Q14_DoAnGroup08.ClientStaff
{
    public partial class frm_Staff_ComputerManagement : Form
    {
        private DatabaseHelper dbHelper;
        private readonly HandlerComputerManagement HandlerCM;
        private System.Windows.Forms.Timer refreshTimer;
        private TcpClient serverClient;
        public frm_Staff_ComputerManagement()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QuanLyQuanNet;Integrated Security=True");
            //Thiet lap timer de lam moi moix giay
            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 5000;
            refreshTimer.Tick += (s, e) => LoadMachines();
            refreshTimer.Start();
            
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frm_Staff_ComputerManagement_Load(object sender, EventArgs e)
        {
            LoadMachines();
            Thread connectThread = new Thread(ConnectToServerAndRegister);
            connectThread.IsBackground = true;
            connectThread.Start();
        }
        private void LoadMachines()
        {
            var handler = new HandlerComputerManagement(dbHelper);
            ComputerManagemetResult result = handler.HandlerGetAllComputersManagement();
            if (result.status == "success")
            {
               if(dgvComputers.InvokeRequired)
                {
                    dgvComputers.Invoke(new MethodInvoker(delegate { dgvComputers.DataSource = result.data; }));
                    labelSummary.Invoke(new MethodInvoker(delegate { labelSummary.Text = result.s; }));
                }
                else
                {
                    dgvComputers.DataSource = result.data;
                    labelSummary.Text = result.s;
                }
            }
            else
            {
                MessageBox.Show("Lỗi khi tải dữ liệu " + result.message);
            }
        }
        public void ConnectToServerAndRegister()
        {
            string clientName = Environment.MachineName;
            try
            {
                serverClient = new TcpClient();
                serverClient.Connect(IPAddress.Parse("127.0.0.1"), 8080);
                NetworkStream networkStream = serverClient.GetStream();
                byte[] nameBytes = Encoding.UTF8.GetBytes(clientName);
                networkStream.Write(nameBytes, 0, nameBytes.Length);
                Console.WriteLine($"Đã gửi tên máy '{clientName}' đến Server.");
                ListenForServerCommands(serverClient);

            }
            catch (Exception ex)
            {
                if(this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        MessageBox.Show("Lỗi kết nối đến server: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }));
                }
                else
                {
                    MessageBox.Show("Lỗi kết nối đến server: " + ex.Message);
                }
            }
        }
       
        private void ListenForServerCommands(TcpClient clientSocket)
        {
            NetworkStream stream = clientSocket.GetStream();
            byte[] buffer = new byte[256];
            int bytesRead;
            try
            {
                while (clientSocket.Connected)
                {
                    if (stream.DataAvailable)
                    {
                        bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string command = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                        HandleServerCommand(command);
                    }
                    else Thread.Sleep(100);
                }
            }
            catch (IOException ex) when (ex.InnerException is SocketException se && se.SocketErrorCode == SocketError.ConnectionReset)
            {
             
                Console.WriteLine("Server đã ngắt kết nối.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong quá trình lắng nghe lệnh: {ex.Message}");
            }
            finally
            {
               
                if (clientSocket.Connected)
                {
                    clientSocket.Close();
                }
            }
        }
        private void HandleServerCommand(string command)
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { HandleServerCommand(command); }));
                return;
            }
            Console.WriteLine($"Nhan lenh tu Server: {command}");
            if(command.Equals("LOCK_PC", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Máy tính đã bị khóa từ Server!");
                frm_LockScreenVer2 lockScreen = new frm_LockScreenVer2();
                this.Hide();
                lockScreen.ShowDialog();
            }
            else if(command.Equals("UNLOCK_PC", StringComparison.OrdinalIgnoreCase))
            {
                LoadMachines();
            }
        }
        private void frm_Staff_ComputerManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (serverClient != null && serverClient.Connected)
                {
                    serverClient.Close();
                }
            }
            catch { }
        }
        private void SendCommandToServer(object data, string action)
        {
            if (serverClient == null || !serverClient.Connected)
            {
                MessageBox.Show("Không thể gửi lệnh: Client chưa kết nối đến Server.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var request = new
                {
                    action = action,
                    data = data
                };
                string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(request);

                NetworkStream stream = serverClient.GetStream();
                byte[] buffer = Encoding.UTF8.GetBytes(jsonRequest);
                stream.Write(buffer, 0, buffer.Length);
                Console.WriteLine($"Đa gui yeu cau: {action}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi gui lenh đen Server: {ex.Message}");
                MessageBox.Show("Lỗi khi gửi lệnh đến Server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void labelSummary_Click(object sender, EventArgs e)
        {

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadMachines();
        }

        private void btnKhoaMay_Click(object sender, EventArgs e)
        {
            if (dgvComputers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một máy tính để khóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string computerId = dgvComputers.CurrentRow.Cells["Mã máy"].Value?.ToString();
            string status = dgvComputers.CurrentRow.Cells["Trạng thái"].Value?.ToString();

            if (computerId == null || status == "Off")
            {
                MessageBox.Show("Máy đã tắt hoặc không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            var data = new
            {
                ComputerId = computerId,
                ActionType = "LOCK"
            };
            
            SendCommandToServer(data, "CONTROL_PC");

         
            MessageBox.Show($"Đã gửi lệnh khóa máy {computerId} đến Server. Server sẽ chuyển tiếp lệnh này đến Máy trạm tương ứng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadMachines();
        }

        private void btnKetThucPhien_Click(object sender, EventArgs e)
        {

            if (dgvComputers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một máy tính để kết thúc phiên chơi.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string computerId = dgvComputers.CurrentRow.Cells["Mã máy"].Value?.ToString();
            string customerId = dgvComputers.CurrentRow.Cells["Khách hàng"].Value?.ToString();
            string status = dgvComputers.CurrentRow.Cells["Trạng thái"].Value?.ToString();

            if (computerId == null || status == "Off" || string.IsNullOrEmpty(customerId))
            {
                MessageBox.Show("Máy đã tắt hoặc không có phiên chơi đang hoạt động.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var data = new
            {
                ComputerId = computerId,
                CustomerId = customerId,
                ActionType = "END_SESSION"
            };

            SendCommandToServer(data, "END_SESSION");

            MessageBox.Show($"Đã gửi yêu cầu kết thúc phiên máy {computerId}. Vui lòng kiểm tra hóa đơn và cập nhật trạng thái máy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadMachines();
        }
    }
}
