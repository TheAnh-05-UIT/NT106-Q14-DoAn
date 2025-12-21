using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public static class PasswordHelper
    {
        public static string HashPassword(string rawPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(rawPassword);
        }

        public static bool VerifyPassword(string rawPassword, string hashedPasswordFromDB)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(rawPassword, hashedPasswordFromDB);
            }
            catch
            {
                return false;
            }
        }
    }
}
