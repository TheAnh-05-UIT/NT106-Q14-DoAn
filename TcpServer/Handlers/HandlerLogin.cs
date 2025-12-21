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
                string rawPassword = (string)data.password; // Mật khẩu người dùng nhập (vd: "123")

                // 1. CHỈ TÌM THEO USERNAME (Bỏ check password ở câu SQL)
                // Phải SELECT cột [Password] ra để đem về so sánh
                string query = "SELECT UserId, FullName, [Role], [Password] FROM Users WHERE Username=@u AND Active=1";

                DataTable dt = db.ExecuteQuery(query, new SqlParameter("@u", username));

                if (dt.Rows.Count > 0)
                {
                    // 2. LẤY MẬT KHẨU MÃ HÓA TỪ DB
                    string dbHash = dt.Rows[0]["Password"].ToString();

                    // 3. DÙNG BCRYPT ĐỂ KIỂM TRA
                    bool isCorrect = PasswordHelper.VerifyPassword(rawPassword, dbHash);

                    if (isCorrect)
                    {
                        // Đăng nhập thành công
                        string role = dt.Rows[0]["Role"].ToString();
                        string userId = dt.Rows[0]["UserId"].ToString();
                        string fullName = dt.Rows[0]["FullName"].ToString();

                        return new { status = "success", role = role, userId = userId, fullName = fullName };
                    }
                }

                // Nếu tìm không thấy user HOẶC mật khẩu sai
                return new { status = "fail", message = "Sai tài khoản hoặc mật khẩu." };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = "Lỗi: " + ex.Message };
            }
        }
    }
}