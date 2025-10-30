using System;
using System.Data;
using System.Data.SqlClient;

namespace TcpServer.Handlers
{
    public class HandlerLogin
    {
        private readonly DatabaseHelper db;

        public HandlerLogin(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }

        public object HandleLogin(dynamic data)
        {
            try
            {
                string username = (string)data.username;
                string password = (string)data.password;

                string query = "SELECT [Role] FROM Users WHERE Username=@user AND [Password]=@pass AND Active=1";
                SqlParameter[] prms = {
                    new SqlParameter("@user", username),
                    new SqlParameter("@pass", password)
                };

                DataTable dt = db.ExecuteQuery(query, prms);
                if (dt.Rows.Count > 0)
                {
                    string role = dt.Rows[0]["Role"].ToString();
                    return new { status = "success", role };
                }
                else
                {
                    return new { status = "fail", message = "Sai tài khoản hoặc mật khẩu." };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in HandlerLogin: {ex.Message}");
                return new { status = "error", message = "Lỗi hệ thống khi đăng nhập." };
            }
        }
    }
}