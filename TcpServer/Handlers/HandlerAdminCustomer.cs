using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer.Handlers
{
    public class HandlerAdminCustomer
    {
        private readonly DatabaseHelper db;

        public HandlerAdminCustomer(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }


        public object HandleGetAllCustomers()
        {
            try
            {
                string query = @"
                    SELECT 
                        Users.UserId AS CustomerID, 
                        Users.FullName AS [Họ tên], 
                        Customers.Balance AS [Số dư], 
                        CASE WHEN Users.Active = 1 THEN 'Active' ELSE 'Inactive' END AS [Trạng thái],
                        Users.Username AS [Tên đăng nhập],
                        Users.[Password] AS [Mật khẩu]
                    FROM Users
                    JOIN Customers ON Users.UserId = Customers.CustomerId
                    WHERE Users.Role = 'CUSTOMER'";
                DataTable dt = db.ExecuteQuery(query);
                return new { status = "success", data = dt };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in HandleGetAllCustomers: {ex.Message}");
                return new { status = "error", message = "Lỗi khi lấy danh sách khách hàng." };
            }
        }

        // THÊM KHÁCH HÀNG (Từ frm_AddCustomer)
        public object HandleAddCustomer(dynamic data)
        {
            // dùng Transaction vì thao tác trên 2 bảng
            using (SqlConnection conn = new SqlConnection(db.ConnectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string userId = "CUS001"; // Mặc định là người đầu tiên

                    // Tìm mã lớn nhất hiện tại
                    string getMaxIdQuery = "SELECT TOP 1 UserId FROM Users WHERE UserId LIKE 'CUS%' ORDER BY UserId DESC";
                    using (SqlCommand cmdGetMax = new SqlCommand(getMaxIdQuery, conn, transaction))
                    {
                        object result = cmdGetMax.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            string lastId = result.ToString();
                            if (int.TryParse(lastId.Substring(3), out int number))
                            {
                                userId = "CUS" + (number + 1).ToString("D3");
                            }
                        }
                    }
                    string fullName = (string)data.fullName;
                    string username = (string)data.username;
                    string password = (string)data.password;
                    bool isActive = ((string)data.status) == "Active";
                    decimal balance = (decimal)data.balance;
                    Console.WriteLine(userId);
                    Console.WriteLine(username);
                    // INSERT vào bảng Users
                    string userQuery = @"INSERT INTO Users 
                                         (UserId, Username, [Password], FullName, [Role], Active) 
                                         VALUES (@UserId, @Username, @Password, @FullName, 'CUSTOMER', @Active)";
                    using (SqlCommand cmdUser = new SqlCommand(userQuery, conn, transaction))
                    {
                        cmdUser.Parameters.AddWithValue("@UserId", userId);
                        cmdUser.Parameters.AddWithValue("@Username", username);
                        cmdUser.Parameters.AddWithValue("@Password", password);
                        cmdUser.Parameters.AddWithValue("@FullName", fullName);
                        cmdUser.Parameters.AddWithValue("@Active", isActive);
                        cmdUser.ExecuteNonQuery();
                    }

                    // INSERT vào bảng Customers
                    string customerQuery = @"INSERT INTO Customers (CustomerId, Balance, RegisterDate) 
                                             VALUES (@UserId, @Balance, GETDATE())";
                    using (SqlCommand cmdCustomer = new SqlCommand(customerQuery, conn, transaction))
                    {
                        cmdCustomer.Parameters.AddWithValue("@UserId", userId);
                        cmdCustomer.Parameters.AddWithValue("@Balance", balance);
                        cmdCustomer.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return new { status = "success", message = "Thêm khách hàng thành công." };
                }
                catch (SqlException sqlEx)
                {
                    transaction.Rollback();
                    if (sqlEx.Number == 2627) // Lỗi Unique key (tên đăng nhập bị trùng)
                    {
                        //return new { status = "error", message = "Lỗi: Tên đăng nhập đã tồn tại." };
                        return new { status = "error", message = "Lỗi SQL: " + sqlEx.Message };
                    }
                    Console.WriteLine($"Error in HandleAddCustomer: {sqlEx.Message}");
                    return new { status = "error", message = "Lỗi SQL: " + sqlEx.Message };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error in HandleAddCustomer: {ex.Message}");
                    return new { status = "error", message = "Lỗi hệ thống: " + ex.Message };
                }
            }
        }

        // CẬP NHẬT KHÁCH HÀNG (Từ Edit Customer)
        public object HandleUpdateCustomer(dynamic data)
        {
            using (SqlConnection conn = new SqlConnection(db.ConnectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Lấy dữ liệu
                    string fullName = (string)data.fullName;
                    string username = (string)data.username;
                    string password = (string)data.password;
                    bool isActive = ((string)data.status) == "Active";
                    decimal balance = (decimal)data.balance;

                    // Lấy UserId từ Username
                    string userId;
                    using (SqlCommand cmdGetId = new SqlCommand("SELECT UserId FROM Users WHERE Username = @Username", conn, transaction))
                    {
                        cmdGetId.Parameters.AddWithValue("@Username", username);
                        var result = cmdGetId.ExecuteScalar();
                        if (result == null)
                            return new { status = "fail", message = "Không tìm thấy người dùng." };
                        userId = result.ToString();
                    }

                    // UPDATE bảng Users
                    string userQuery = @"UPDATE Users 
                                         SET FullName = @FullName, [Password] = @Password, Active = @Active 
                                         WHERE UserId = @UserId";
                    using (SqlCommand cmdUser = new SqlCommand(userQuery, conn, transaction))
                    {
                        cmdUser.Parameters.AddWithValue("@FullName", fullName);
                        cmdUser.Parameters.AddWithValue("@Password", password);
                        cmdUser.Parameters.AddWithValue("@Active", isActive);
                        cmdUser.Parameters.AddWithValue("@UserId", userId);
                        cmdUser.ExecuteNonQuery();
                    }

                    // UPDATE bảng Customers
                    string customerQuery = "UPDATE Customers SET Balance = @Balance WHERE CustomerId = @UserId";
                    using (SqlCommand cmdCustomer = new SqlCommand(customerQuery, conn, transaction))
                    {
                        cmdCustomer.Parameters.AddWithValue("@Balance", balance);
                        cmdCustomer.Parameters.AddWithValue("@UserId", userId);
                        cmdCustomer.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return new { status = "success", message = "Cập nhật thành công." };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error in HandleUpdateCustomer: {ex.Message}");
                    return new { status = "error", message = "Lỗi khi cập nhật: " + ex.Message };
                }
            }
        }

        // XÓA KHÁCH HÀNG
        public object HandleDeleteCustomer(dynamic data)
        {
            try
            {
                string customerId = (string)data.customerId;

                string query = "UPDATE Users SET Active = 0 WHERE UserId = @UserId";
                int rows = db.ExecuteNonQuery(query,
                    new SqlParameter("@UserId", customerId)
                );

                if (rows > 0)
                    return new { status = "success", message = "Vô hiệu hóa tài khoản thành công." };
                else
                    return new { status = "fail", message = "Không tìm thấy tài khoản." };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in HandleDeleteCustomer: {ex.Message}");
                return new { status = "error", message = "Lỗi khi xóa: " + ex.Message };
            }
        }

        // NẠP TIỀN (Từ frm_Deposit)
        public object HandleDeposit(dynamic data)
        {
            // Cập nhật Balance VÀ ghi vào TopUpTransactions
            using (SqlConnection conn = new SqlConnection(db.ConnectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // Lấy dữ liệu
                    string username = (string)data.username;
                    decimal amount = (decimal)data.amount;
                    string employeeId = (string)data.employeeId;
                    if (string.IsNullOrEmpty(employeeId))
                    {
                        return new { status = "error", message = "Lỗi: Không xác định được nhân viên thực hiện." };
                    }
                    // Lấy CustomerId từ Username
                    string customerId;
                    using (SqlCommand cmdGetId = new SqlCommand("SELECT UserId FROM Users WHERE Username = @Username", conn, transaction))
                    {
                        cmdGetId.Parameters.AddWithValue("@Username", username);
                        var result = cmdGetId.ExecuteScalar();
                        if (result == null)
                            return new { status = "fail", message = "Không tìm thấy người dùng." };
                        customerId = result.ToString();
                    }

                    // Cập nhật Balance trong bảng Customers
                    string updateQuery = @"UPDATE Customers SET Balance = Balance + @Amount 
                                           WHERE CustomerId = @CustomerId";
                    using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn, transaction))
                    {
                        cmdUpdate.Parameters.AddWithValue("@Amount", amount);
                        cmdUpdate.Parameters.AddWithValue("@CustomerId", customerId);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    // Ghi log vào bảng TopUpTransactions
                    string transQuery = @"INSERT INTO TopUpTransactions 
                                          (TransactionId, CustomerId, EmployeeId, Amount) 
                                          VALUES (@TransId, @CustomerId, @EmployeeId, @Amount)";
                    using (SqlCommand cmdTrans = new SqlCommand(transQuery, conn, transaction))
                    {
                        cmdTrans.Parameters.AddWithValue("@TransId", "T" + Guid.NewGuid().ToString("N").Substring(0, 9));
                        cmdTrans.Parameters.AddWithValue("@CustomerId", customerId);
                        cmdTrans.Parameters.AddWithValue("@EmployeeId", employeeId);
                        cmdTrans.Parameters.AddWithValue("@Amount", amount);
                        cmdTrans.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return new { status = "success", message = "Nạp tiền thành công." };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error in HandleDeposit: {ex.Message}");
                    return new { status = "error", message = "Lỗi hệ thống khi nạp tiền." };
                }
            }
        }
        // TÌM KIẾM KHÁCH HÀNG
        public object HandleSearchCustomer(dynamic data)
        {
            try
            {
                string keyword = (string)data.keyword;

                // Thêm dấu % để tìm kiếm ở mọi vị trí
                string searchPattern = "%" + keyword + "%";

                string query = @"
            SELECT 
                Users.UserId AS CustomerID, 
                Users.FullName AS [Họ tên], 
                Customers.Balance AS [Số dư], 
                CASE WHEN Users.Active = 1 THEN 'Active' ELSE 'Inactive' END AS [Trạng thái],
                Users.Username AS [Tên đăng nhập],
                Users.[Password] AS [Mật khẩu]
            FROM Users
            JOIN Customers ON Users.UserId = Customers.CustomerId
            WHERE Users.Role = 'CUSTOMER' 
            AND (Users.FullName LIKE @Keyword OR Users.Username LIKE @Keyword)";
                // Tìm cả trong Tên hiển thị VÀ Tên đăng nhập

                DataTable dt = db.ExecuteQuery(query, new SqlParameter("@Keyword", searchPattern));

                return new { status = "success", data = dt };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in HandleSearchCustomer: {ex.Message}");
                return new { status = "error", message = "Lỗi khi tìm kiếm." };
            }
        }
    }
}
