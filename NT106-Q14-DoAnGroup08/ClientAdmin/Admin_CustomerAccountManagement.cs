//using Microsoft.IdentityModel.Protocols;
//using NT106_Q14_DoAnGroup08.ClientCustomer;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Security;
//using System.Windows.Forms;
//using System.Windows.Forms.Design;
//using TcpServer;

//namespace NT106_Q14_DoAnGroup08.ClientAdmin
//{
//    public partial class Admin_CustomerAccountManagement : Form
//    {
//        private DatabaseHelper db;
//        private string currentRole;

//        //public Admin_CustomerAccountManagement()
//        //{
//        //    InitializeComponent();
//        //}
//        public Admin_CustomerAccountManagement()
//        {
//            //string role
//            InitializeComponent();
//            //currentRole = role;
//            string connStr = ConfigurationManager.ConnectionStrings["QuanLyQuanNet"].ConnectionString;
//            db = new DatabaseHelper(connStr);
//            // db = new DatabaseHelper(ConfigurationManager.ConnectionStrings["QuanLyQuanNet"].ConnectionString);
//        }

//        //public Admin_CustomerAccountManagement()
//        //{
//        //}

//        private void lblTitle_Click(object sender, EventArgs e)
//        {

//        }

//        private void LoadData()
//        {
//            // DÙNG JOIN ĐỂ LẤY THÔNG TIN TỪ CẢ 2 BẢNG
//            string query = @"
//            SELECT u.UserId AS [Customer ID], u.FullName AS [Họ tên], c.Balance AS [Số dư],
//            CASE WHEN u.Active = 1 THEN 'Active' ELSE 'Inactive' END AS [Trạng thái],
//            u.Username AS [Tên đăng nhập], u.[Password] AS [Mật khẩu]
//            FROM Users u
//            JOIN Customers c ON u.UserId = c.CustomerId
//            WHERE u.Role = 'CUSTOMER'";
//            dgvAccCustomers.DataSource = db.ExecuteQuery(query);
//        }

//        private void Admin_CustomerAccountManagement_Load(object sender, EventArgs e)
//        {
//            LoadData();
//            if (currentRole != "ADMIN")
//            {
//                btnAddAccount.Enabled = false; // Chỉ ADMIN được thêm
//            }
//            //dgvAccCustomers.Rows.Add("KH01", "Nguyen Van A", "50000", "Active", "UserA", "userA@123");
//            //dgvAccCustomers.Rows.Add("KH02", "Tran Thi B", "0", "Inactive", "UserB", "userB@123");
//            //dgvAccCustomers.Rows.Add("KH03", "Le Van C", "120000", "Active", "UserC", "userC@123");
//        }

//        private string GenerateCustomerId(DatabaseHelper db)
//        {
//            // Lấy mã khách hàng lớn nhất hiện có
//            string query = "SELECT TOP 1 CustomerId FROM Customers ORDER BY CustomerId DESC";
//            var dt = db.ExecuteQuery(query);

//            if (dt.Rows.Count == 0)
//                return "CUS001"; // Nếu chưa có khách hàng nào

//            string lastId = dt.Rows[0]["CustomerId"].ToString(); // VD: "CUS007"
//            int number = int.Parse(lastId.Substring(3));         // Lấy phần số: 007 -> 7
//            number++;                                            // Tăng 1 -> 8
//            return "CUS" + number.ToString("D3");                // Ghép lại: "CUS008"
//        }

//        private void btnAddAccount_Click(object sender, EventArgs e)
//        {
//            if (currentRole != "ADMIN")
//            {
//                MessageBox.Show("Bạn không có quyền thêm tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            frm_AddCustomer addUser = new frm_AddCustomer();
//            if (addUser.ShowDialog() == DialogResult.OK)
//            {
//                // 1. Lấy ID mới từ hàm bạn đã viết
//                string newCustomerId = GenerateCustomerId(db);

//                // 2. INSERT vào bảng Users
//                string userQuery = "INSERT INTO Users (UserId, FullName, Active, Username, [Password], [Role]) " +
//                                   "VALUES (@UserId, @FullName, @Active, @Username, @Password, 'CUSTOMER')";
//                SqlParameter[] userPrms = {
//                    new SqlParameter("@UserId", newCustomerId),
//                    new SqlParameter("@FullName", addUser.CustomerName),
//                    new SqlParameter("@Active", addUser.Status == "Active" ? 1 : 0),
//                    new SqlParameter("@Username", addUser.userName),
//                    new SqlParameter("@Password", addUser.userPassword)
//                };
//                db.ExecuteNonQuery(userQuery, userPrms);

//                // 3. INSERT vào bảng Customers
//                string customerQuery = "INSERT INTO Customers (CustomerId, Balance) VALUES (@CustomerId, @Balance)";
//                SqlParameter[] customerPrms = {
//                    new SqlParameter("@CustomerId", newCustomerId),
//                    new SqlParameter("@Balance", addUser.Balance)
//                };
//                int rows = db.ExecuteNonQuery(customerQuery, customerPrms);

//                MessageBox.Show(rows > 0 ? "Thêm thành công!" : "Thêm thất bại!");
//                LoadData();
//            }


//            //ClientCustomer.frm_AddCustomer addUser = new ClientCustomer.frm_AddCustomer();
//            //if (addUser.ShowDialog() == DialogResult.OK)
//            //{
//            //    string name = addUser.CustomerName;
//            //    string balance = addUser.Balance;
//            //    string status = addUser.Status;
//            //    string username = addUser.userName;
//            //    string userpass = addUser.userPassword;
//            //    string id = "KH" + (dgvAccCustomers.Rows.Count).ToString("00");
//            //    dgvAccCustomers.Rows.Add(id, name, balance, status, username, userpass);
//            //}
//        }

//        private void btnRepair_Click(object sender, EventArgs e)
//        {
//            if (dgvAccCustomers.CurrentRow == null)
//            {
//                MessageBox.Show("Vui lòng chọn thông khách hàng để sửa!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }
//            DataGridViewRow selectedRow = dgvAccCustomers.CurrentRow;
//            string id = selectedRow.Cells["colID"].Value.ToString();
//            string name = selectedRow.Cells["colName"].Value.ToString();
//            string balance = selectedRow.Cells["colBalance"].Value.ToString();
//            string username = selectedRow.Cells["colUser"].Value.ToString();
//            string userpassword = selectedRow.Cells["colPass"].Value.ToString();
//            string status = selectedRow.Cells["colStatus"].Value.ToString();
//            ClientCustomer.frm_AddCustomer editFrom = new ClientCustomer.frm_AddCustomer();
//            editFrom.Text = "Edit Customer";
//            editFrom.LoadCustomerData(name, balance, status, username, userpassword);
//            if (editFrom.ShowDialog() == DialogResult.OK)
//            {
//                // TẠO LỆNH UPDATE CHO BẢNG USERS
//                string userQuery = "UPDATE Users SET FullName=@FullName, Active=@Active, Username=@Username, Password=@Password WHERE UserId=@UserId";
//                SqlParameter[] userPrms = {
//                    new SqlParameter("@FullName", editFrom.CustomerName),
//                    new SqlParameter("@Active", editFrom.Status == "Active" ? 1 : 0),
//                    new SqlParameter("@Username", editFrom.userName),
//                    new SqlParameter("@Password", editFrom.userPassword),
//                    new SqlParameter("@UserId", id)
//                };
//                db.ExecuteNonQuery(userQuery, userPrms);

//                // TẠO LỆNH UPDATE CHO BẢNG CUSTOMERS
//                string customerQuery = "UPDATE Customers SET Balance=@Balance WHERE CustomerId=@CustomerId";
//                SqlParameter[] customerPrms = {
//                     new SqlParameter("@Balance", editFrom.Balance),
//                     new SqlParameter("@CustomerId", id)
//                };
//                int rows = db.ExecuteNonQuery(customerQuery, customerPrms);

//                MessageBox.Show(rows > 0 ? "Cập nhật thành công!" : "Cập nhật thất bại!");
//                LoadData(); // Tải lại dữ liệu từ DB
//            }


//            //if (dgvAccCustomers.SelectedRows.Count == 0)
//            //{
//            //    MessageBox.Show("Chọn tài khoản để sửa!");
//            //    return;
//            //}

//            //var row = dgvAccCustomers.SelectedRows[0];
//            //frm_EditCustomer frm = new frm_EditCustomer
//            //{
//            //    FullName = row.Cells["Họ tên"].Value.ToString(),
//            //    Balance = Convert.ToDecimal(row.Cells["Số dư"].Value),
//            //    Status = row.Cells["Trạng thái"].Value.ToString(),
//            //    Username = row.Cells["Tên đăng nhập"].Value.ToString(),
//            //    Password = row.Cells["Mật khẩu"].Value.ToString()
//            //};

//            //if (frm.ShowDialog() == DialogResult.OK)
//            //{
//            //    string query = "UPDATE Users SET FullName=@FullName, Balance=@Balance, Active=@Active, Username=@Username, [Password]=@Password WHERE UserId=@UserId";
//            //    SqlParameter[] prms = {
//            //        new SqlParameter("@FullName", frm.FullName),
//            //        new SqlParameter("@Balance", frm.Balance),
//            //        new SqlParameter("@Active", frm.Status == "Active" ? 1 : 0),
//            //        new SqlParameter("@Username", frm.Username),
//            //        new SqlParameter("@Password", frm.Password),
//            //        new SqlParameter("@UserId", row.Cells["Customer ID"].Value.ToString())
//            //    };

//            //    int rows = db.ExecuteNonQuery(query, prms);
//            //    MessageBox.Show(rows > 0 ? "Cập nhật thành công!" : "Cập nhật thất bại!");
//            //    LoadData();
//            //}

//        }

//        private void btnDelete_Click(object sender, EventArgs e)
//        {
//            //if (dgvAccCustomers.CurrentRow != null)
//            //{
//            //    DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này.", "Thông báo!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
//            //    if (rs == DialogResult.OK)
//            //    {
//            //        dgvAccCustomers.Rows.Remove(dgvAccCustomers.CurrentRow);
//            //    }
//            //}
//            //else
//            //{
//            //    MessageBox.Show("Vui lòng chọn hàng muốn xóa.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            //}

//            if (dgvAccCustomers.SelectedRows.Count == 0)
//            {
//                MessageBox.Show("Chọn tài khoản để xóa!");
//                return;
//            }

//            var userId = dgvAccCustomers.SelectedRows[0].Cells["Customer ID"].Value.ToString();
//            // THAY ĐỔI LỆNH DELETE THÀNH UPDATE
//            string query = "UPDATE Users SET Active = 0 WHERE UserId=@UserId";
//            SqlParameter[] prms = { new SqlParameter("@UserId", userId) };

//            int rows = db.ExecuteNonQuery(query, prms);
//            MessageBox.Show(rows > 0 ? "Vô hiệu hóa tài khoản thành công!" : "Thao tác thất bại!");
//            LoadData();

//        }

//        private void btnNaptien_Click(object sender, EventArgs e)
//        {
//            //if (dgvAccCustomers.CurrentRow != null)
//            //{
//            //    DataGridViewRow selectedRow = dgvAccCustomers.CurrentRow;
//            //    string balance = selectedRow.Cells["colBalance"].Value.ToString();
//            //    string username = selectedRow.Cells["colUser"].Value.ToString();
//            //    string status = selectedRow.Cells["colStatus"].Value.ToString();
//            //    ClientCustomer.frm_Deposit depositt = new ClientCustomer.frm_Deposit();

//            //    depositt.LoadCustomerData(balance, status, username);

//            //    if (depositt.ShowDialog() == DialogResult.OK)
//            //    {
//            //        selectedRow.Cells["colBalance"].Value = depositt.Balance;
//            //        MessageBox.Show("Đã nạp tiền thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            //    }
//            //}
//            //else
//            //{
//            //    MessageBox.Show("Vui lòng chọn tài khoản khác hàng mà bạn muốn nạp tiền.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            //}


//            if (dgvAccCustomers.SelectedRows.Count == 0)
//            {
//                MessageBox.Show("Chọn tài khoản để nạp tiền!");
//                return;
//            }

//            var userId = dgvAccCustomers.SelectedRows[0].Cells["Customer ID"].Value.ToString();
//            decimal amount = 50000; // nên lấy từ một ô nhập liệu

//            // SỬA LẠI BẢNG CUSTOMERS
//            string queryUpdate = "UPDATE Customers SET Balance = Balance + @Amount WHERE CustomerId=@UserId";
//            SqlParameter[] prmsUpdate = {
//        new SqlParameter("@Amount", amount),
//        new SqlParameter("@UserId", userId)
//    };
//            int rows = db.ExecuteNonQuery(queryUpdate, prmsUpdate);

//            // THÊM LOG GIAO DỊCH
//            if (rows > 0)
//            {
//                string queryLog = "INSERT INTO TopUpTransactions (TransactionId, CustomerId, EmployeeId, Amount) VALUES (@TID, @CID, @EID, @Amount)";
//                SqlParameter[] prmsLog = {
//            new SqlParameter("@TID", "T" + DateTime.Now.ToString("yyyyMMddHHmmss")),
//            new SqlParameter("@CID", userId),
//            new SqlParameter("@EID", "AD001"), // Tạm thời, nên lấy ID của admin/nhân viên đang đăng nhập
//            new SqlParameter("@Amount", amount)
//        };
//                db.ExecuteNonQuery(queryLog, prmsLog);
//                MessageBox.Show("Nạp tiền thành công!");
//            }
//            else
//            {
//                MessageBox.Show("Nạp tiền thất bại!");
//            }

//            LoadData();

//        }
//        private void btnRefresh_Click(object sender, EventArgs e)
//        {
//            LoadData();
//        }
//    }
//}





using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace NT106_Q14_DoAnGroup08.ClientAdmin
{
    public partial class Admin_CustomerAccountManagement : Form
    {
        public Admin_CustomerAccountManagement()
        {
            InitializeComponent();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void Admin_CustomerAccountManagement_Load(object sender, EventArgs e)
        {


            dgvAccCustomers.Rows.Add("KH01", "Nguyen Van A", "50000", "Active", "UserA", "userA@123");
            dgvAccCustomers.Rows.Add("KH02", "Tran Thi B", "0", "Inactive", "UserB", "userB@123");
            dgvAccCustomers.Rows.Add("KH03", "Le Van C", "120000", "Active", "UserC", "userC@123");


        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            ClientCustomer.frm_AddCustomer addUser = new ClientCustomer.frm_AddCustomer();
            if (addUser.ShowDialog() == DialogResult.OK)
            {
                string name = addUser.CustomerName;
                string balance = addUser.Balance;
                string status = addUser.Status;
                string username = addUser.userName;
                string userpass = addUser.userPassword;
                string id = "KH" + (dgvAccCustomers.Rows.Count).ToString("00");
                dgvAccCustomers.Rows.Add(id, name, balance, status, username, userpass);
            }
        }

        private void btnRepair_Click(object sender, EventArgs e)
        {
            if (dgvAccCustomers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thông khách hàng để sửa!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow selectedRow = dgvAccCustomers.CurrentRow;
            string id = selectedRow.Cells["colID"].Value.ToString();
            string name = selectedRow.Cells["colName"].Value.ToString();
            string balance = selectedRow.Cells["colBalance"].Value.ToString();
            string username = selectedRow.Cells["colUser"].Value.ToString();
            string userpassword = selectedRow.Cells["colPass"].Value.ToString();
            string status = selectedRow.Cells["colStatus"].Value.ToString();
            ClientCustomer.frm_AddCustomer editFrom = new ClientCustomer.frm_AddCustomer();
            editFrom.Text = "Edit Customer";
            editFrom.LoadCustomerData(name, balance, status, username, userpassword);
            if (editFrom.ShowDialog() == DialogResult.OK)
            {
                selectedRow.Cells["colName"].Value = editFrom.CustomerName;
                selectedRow.Cells["colBalance"].Value = editFrom.Balance;
                selectedRow.Cells["colUser"].Value = editFrom.userName;
                selectedRow.Cells["colPass"].Value = editFrom.userPassword;
                selectedRow.Cells["colStatus"].Value = editFrom.Status;
                MessageBox.Show("Dữ liệu đã được thay đổi!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAccCustomers.CurrentRow != null)
            {
                DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này.", "Thông báo!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rs == DialogResult.OK)
                {
                    dgvAccCustomers.Rows.Remove(dgvAccCustomers.CurrentRow);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng muốn xóa.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNaptien_Click(object sender, EventArgs e)
        {
            if (dgvAccCustomers.CurrentRow != null)
            {
                DataGridViewRow selectedRow = dgvAccCustomers.CurrentRow;
                string balance = selectedRow.Cells["colBalance"].Value.ToString();
                string username = selectedRow.Cells["colUser"].Value.ToString();
                string status = selectedRow.Cells["colStatus"].Value.ToString();
                ClientCustomer.frm_Deposit depositt = new ClientCustomer.frm_Deposit();

                depositt.LoadCustomerData(balance, status, username);

                if (depositt.ShowDialog() == DialogResult.OK)
                {
                    selectedRow.Cells["colBalance"].Value = depositt.Balance;
                    MessageBox.Show("Đã nạp tiền thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tài khoản khác hàng mà bạn muốn nạp tiền.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
        }
    }
}