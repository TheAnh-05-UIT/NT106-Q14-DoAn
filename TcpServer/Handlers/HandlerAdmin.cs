using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer.Handlers
{
    public class HandlerAdmin
    {
        private readonly DatabaseHelper db;

        public HandlerAdmin(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }

        public object HandleGetAdminInfo(dynamic data)
        {
            try
            {
                string userid = ((string)data.userId).Trim();                
                string query = $"SELECT Username, FullName FROM Users WHERE UserId = '{userid}'";
                DataTable dt = db.ExecuteQuery(query);
                DataRow row = (dt.Rows.Count > 0) ? dt.Rows[0] : null;
                if (row != null)
                {
                    var adminDetails = new
                    {
                        UserName = row["Username"].ToString(),
                        FullName = row["FullName"].ToString(),
                       // AdminRole = row["Role"].ToString()
                    };
                    return new { status = "success", data = adminDetails };
                }
                return new { status = "error", message = "Admin not found." };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }
    }
}