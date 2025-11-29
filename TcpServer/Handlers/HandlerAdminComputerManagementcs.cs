using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer.Handlers
{
    public class HandlerAdminComputerManagementcs
    {
        private readonly DatabaseHelper db;
        public HandlerAdminComputerManagementcs(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }
        // HandlerAdminComputerManagementcs.cs

        public object HandleGetComputerDetails(dynamic data)
        {
            try
            {
                string id = ((string)data.ComputerId).Trim();

                string query = "SELECT ComputerId, ComputerName, [Status], IpAddress, PricePerHour FROM Computers WHERE ComputerId = @Id";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter("@Id", id));
                DataRow row = (dt.Rows.Count > 0) ? dt.Rows[0] : null;

                if (row != null)
                {
                    // Chuyển DataRow thành một đối tượng ẩn danh (anonymous object)
                    var computerDetails = new
                    {
                        ComputerId = row["ComputerId"].ToString(),
                        ComputerName = row["ComputerName"].ToString(),
                        Status = row["Status"].ToString(),
                        IpAddress = row["IpAddress"].ToString(),
                        PricePerHour = (decimal)row["PricePerHour"] // Ép kiểu về decimal
                    };
                    return new { status = "success", data = computerDetails };
                }
                return new { status = "error", message = "Computer not found." };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }
        public object HandleGetAllComputers()
        {
            try
            {
                string query = "SELECT ComputerId, ComputerName, [Status], IpAddress FROM Computers";
                DataTable dt = db.ExecuteQuery(query);
                return new { status = "success", data = dt };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }
        // HandlerAdminComputerManagementcs.cs
        // ... (các hàm khác)

        public object HandleUpdateComputer(dynamic data)
        {
            try
            {
                // Lấy dữ liệu và loại bỏ khoảng trắng thừa
                string id = ((string)data.ComputerId).Trim();
                string name = ((string)data.ComputerName).Trim();
                string ipAddress = ((string)data.IpAddress).Trim();
                // Cần đảm bảo Status được truyền vào khớp CHÍNH XÁC: AVAILABLE, IN_USE, MAINTENANCE
                string status = ((string)data.Status).Trim().ToUpper();
                decimal pricePerHour = (decimal)data.PricePerHour;

                string query = "UPDATE Computers SET " +
                               "ComputerName = @Name, " +
                               "[Status] = @Status, " +
                               "IpAddress = @IpAddress, " +
                               "PricePerHour = @Price " +
                               "WHERE ComputerId = @Id";

                int rows = db.ExecuteNonQuery(query,
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Name", name),
                    new SqlParameter("@Status", status),
                    new SqlParameter("@IpAddress", ipAddress),
                    new SqlParameter("@Price", pricePerHour));

                if (rows > 0) return new { status = "success" };

                // Trả về lỗi nếu không tìm thấy ID máy
                return new { status = "error", message = "Computer not found or no changes made." };
            }
            catch (Exception ex)
            {
                // Trả về lỗi nếu xung đột CHECK constraint (CK_Status)
                return new { status = "error", message = ex.Message };
            }
        }
        public object HandleAddComputer(dynamic data)
        {
            try
            {
                string id = (string)data.ComputerId;
                string name = (string)data.ComputerName;
                string ipAddress = (string)data.IpAddress;
                string status = (string)data.Status;
                decimal pricePerHour = (decimal)data.PricePerHour;
                string query = "INSERT INTO Computers (ComputerId, ComputerName, [Status], IpAddress, PricePerHour) " +
                               "VALUES (@Id, @Name, @Status, @IpAddress, @price)";
                int rows = db.ExecuteNonQuery(query,
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Name", name),
                    new SqlParameter("@Status", status),
                    new SqlParameter("@IpAddress", ipAddress),
                    new SqlParameter("@price", pricePerHour));
                if (rows > 0) return new { status = "success" };
                return new { status = "error", message = "Failed to add computer" };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }
        public object HandleDeleteComputer(dynamic data)
        {
            try
            {
                string id = (string)data.ComputerId;
                string queryInvoices = "DELETE FROM Invoices WHERE SessionId IN " +
                                "(SELECT SessionId FROM Sessions WHERE ComputerId = @Id)";
                db.ExecuteNonQuery(queryInvoices, new SqlParameter("@Id", id));
                string querySession = "DELETE FROM Sessions WHERE ComputerId = @Id";
                db.ExecuteNonQuery(querySession, new SqlParameter("@Id", id));
                string query = "DELETE FROM Computers WHERE ComputerId = @Id";
                int rows = db.ExecuteNonQuery(query,
                    new SqlParameter("@Id", id));
                if (rows > 0) return new { status = "success" };
                return new { status = "error", message = "Computer not found" };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }

        // CẬP NHẬT TRẠNG THÁI MÁY (Available / Maintenance / In_Use)
        public object HandleUpdateStatus(dynamic data)
        {
            try
            {
                string id = (string)data.ComputerId;
                string status = (string)data.Status;

                string query = "UPDATE Computers SET [Status] = @Status WHERE ComputerId = @Id";
                int rows = db.ExecuteNonQuery(query,
                    new SqlParameter("@Status", status),
                    new SqlParameter("@Id", id));

                if (rows > 0) return new { status = "success" };
                return new { status = "error", message = "Computer not found" };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }
    }
}
