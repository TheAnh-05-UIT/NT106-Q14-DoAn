using System;
using System.Data;
using System.Data.SqlClient;

namespace TcpServer.Handlers
{
    public class HandlerComputerManagement
    {
        private readonly DatabaseHelper db;

        public HandlerComputerManagement(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }

        // LẤY DANH SÁCH TẤT CẢ MÁY
        public object HandleGetAllComputers()
        {
            try
            {
                string query = "SELECT ComputerId, ComputerName, [Status] FROM Computers";
                DataTable dt = db.ExecuteQuery(query);
                return new { status = "success", data = dt };
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