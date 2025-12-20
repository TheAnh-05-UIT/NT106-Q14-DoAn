using System.Windows.Forms;

namespace NewNet_Customer.DTO
{
    public static class UserSession
    {
        public static string UserId { get; set; }
        public static string UserName { get; set; }
        public static string FullName { get; set; }
        public static string Role { get; set; }

        public static decimal Balance { get; set; }
        public static Form NextForm { get; set; }
        // Hàm để xóa thông tin khi Đăng xuất
        public static void Clear()
        {
            UserId = null;
            UserName = null;
            FullName = null;
            Role = null;
            NextForm = null;
        }
    }
}
