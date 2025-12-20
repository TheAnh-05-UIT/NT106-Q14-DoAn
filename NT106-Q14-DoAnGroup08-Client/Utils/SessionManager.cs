using System;

namespace QuanLyQuanNet.Utils
{
    public static class SessionManager
    {
        // Simple in-memory session properties for current account
        public static string Username { get; set; }
        public static string FullName { get; set; }
        public static string Role { get; set; }
        public static DateTime? LastLogin { get; set; }

        public static void Clear()
        {
            Username = null;
            FullName = null;
            Role = null;
            LastLogin = null;
        }
    }
}
