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

        public object HandleGetComputerDetails(dynamic data)
        {
            try
            {
                string id = ((string)data.ComputerId).Trim();

                string query = "SELECT ComputerId, ComputerName, [Status], PricePerHour FROM Computers WHERE ComputerId = @Id";
                DataTable dt = db.ExecuteQuery(query, new SqlParameter("@Id", id));
                DataRow row = (dt.Rows.Count > 0) ? dt.Rows[0] : null;

                if (row != null)
                {
                    var computerDetails = new
                    {
                        ComputerId = row["ComputerId"].ToString(),
                        ComputerName = row["ComputerName"].ToString(),
                        Status = row["Status"].ToString(),
                        PricePerHour = (decimal)row["PricePerHour"] 
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
                string query = "SELECT ComputerId, ComputerName, [Status] FROM Computers";
                DataTable dt = db.ExecuteQuery(query);
                return new { status = "success", data = dt };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }


        public object HandleUpdateComputer(dynamic data)
        {
            try
            {
                string id = ((string)data.ComputerId).Trim();
                string name = ((string)data.ComputerName).Trim();
                string status = ((string)data.Status).Trim().ToUpper();
                decimal pricePerHour = (decimal)data.PricePerHour;

                string query = "UPDATE Computers SET " +
                               "ComputerName = @Name, " +
                               "[Status] = @Status, " +
                               "PricePerHour = @Price " +
                               "WHERE ComputerId = @Id";

                int rows = db.ExecuteNonQuery(query,
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Name", name),
                    new SqlParameter("@Status", status),
                    new SqlParameter("@Price", pricePerHour));

                if (rows > 0) return new { status = "success" };
                return new { status = "error", message = "Computer not found or no changes made." };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }
        public object HandleAddComputer(dynamic data)
        {
            try
            {
                string id = (string)data.ComputerId;
                string name = (string)data.ComputerName;
                string status = (string)data.Status;
                decimal pricePerHour = (decimal)data.PricePerHour;
                string query = "INSERT INTO Computers (ComputerId, ComputerName, [Status], PricePerHour) " +
                               "VALUES (@Id, @Name, @Status, @price)";
                int rows = db.ExecuteNonQuery(query,
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Name", name),
                    new SqlParameter("@Status", status),
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
