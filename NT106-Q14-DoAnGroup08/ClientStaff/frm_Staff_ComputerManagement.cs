using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using TcpServer;

namespace NT106_Q14_DoAnGroup08.ClientStaff
{
    public partial class frm_Staff_ComputerManagement : Form
    {
        private DatabaseHelper dbHelper;
        private Timer refreshTimer;
        public frm_Staff_ComputerManagement()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QuanLyQuanNet;Integrated Security=True");
            //Thiet lap timer de lam moi moix giay
            refreshTimer = new Timer();
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
        }
        private void LoadMachines()
        {
            try
            {
                string query = "SELECT co.ComputerId as [Mã máy], co.Status as [Trạng thái], se.StartTime as [Thời gian bắt đầu], cu.CustomerId as [Khách hàng], cu.Balance as [Số dư] FROM Computers co LEFT JOIN Sessions se ON co.ComputerId = se.ComputerId  LEFT JOIN Customers cu ON CU.CustomerId = se.CustomerId";
                string query2 = "SELECT COUNT(*) FROM Computers WHERE Status = 'On'";
                string query1 = "SELECT COUNT(*) FROM Computers";
                int rs1 = (int)(dbHelper.ExecuteScalar(query1));
                int rs2 = (int)(dbHelper.ExecuteScalar(query2));
                labelSummary.Text = $"Active: {rs2}/{rs1}";
                DataTable dt = dbHelper.ExecuteQuery(query);
                dgvComputers.DataSource = dt;
                
               // dgvComputers.InvokeIfRequired(() => dgvComputers.DataSource = dt);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }

        private void labelSummary_Click(object sender, EventArgs e)
        {

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadMachines();
        }
    }
}
