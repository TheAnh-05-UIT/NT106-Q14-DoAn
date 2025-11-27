//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TcpServer.Handlers
//{
//    public class ComputerManagemetResult
//    {
//        public string status { get; set; }
//        public DataTable data { get; set; }
//        public string s { get; set; }
//        public string message { get; set; }
//    }
//    public class HandlerComputerManagement
//    {
//        private readonly DatabaseHelper db;
//        public HandlerComputerManagement(DatabaseHelper databaseHelper)
//        {
//            db = databaseHelper;
//        }
//        public void HandlerLockComputer(int computerId)
//        {
//            string query = "UPDATE Computers SET Status='Off' WHERE ComputerId=@compId";
//            var prms = new System.Data.SqlClient.SqlParameter[]
//            {
//                new System.Data.SqlClient.SqlParameter("@compId", computerId)
//            };
//            db.ExecuteNonQuery(query, prms);
//        }
//        public ComputerManagemetResult HandlerGetAllComputersManagement()
//        {
//            try
//            {
//                string query = "SELECT co.ComputerId as [Mã máy], co.Status as [Trạng thái], se.StartTime as [Thời gian bắt đầu], cu.CustomerId as [Khách hàng], cu.Balance as [Số dư] FROM Computers co LEFT JOIN Sessions se ON co.ComputerId = se.ComputerId  LEFT JOIN Customers cu ON CU.CustomerId = se.CustomerId";
//                DataTable dt = db.ExecuteQuery(query);
//                string query2 = "SELECT COUNT(*) FROM Computers WHERE Status = 'On'";
//                string query1 = "SELECT COUNT(*) FROM Computers";
//                int rs1 = (int)(db.ExecuteScalar(query1));
//                int rs2 = (int)(db.ExecuteScalar(query2));
//                string text = $"Active: {rs2}/{rs1}";
//                return new ComputerManagemetResult
//                {
//                    status = "success", data = dt, s = text
//                };
//            }
//            catch(Exception ex)
//            {
//                return new ComputerManagemetResult
//                {
//                    status = "error",
//                    message = ex.Message
//                };
//            }
//        }

//    }
//}



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

        // 1. LẤY DANH SÁCH TẤT CẢ MÁY
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

        // 2. CẬP NHẬT TRẠNG THÁI MÁY (Available / Maintenance / In_Use)
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