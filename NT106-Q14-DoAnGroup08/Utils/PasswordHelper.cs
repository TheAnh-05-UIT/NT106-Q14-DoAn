using System;
using System.Security.Cryptography;

namespace QuanLyQuanNet.Utils
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);
            const int iter = 10000;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iter))
            {
                var hash = pbkdf2.GetBytes(32);
                return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}:{iter}";
            }
        }

        public static bool VerifyPassword(string password, string stored)
        {
            var parts = stored.Split(':');
            if (parts.Length != 3) return false;
            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);
            var iter = int.Parse(parts[2]);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iter))
            {
                var computed = pbkdf2.GetBytes(hash.Length);
                if (computed.Length != hash.Length) return false;
                // constant-time comparison
                int diff = 0;
                for (int i = 0; i < hash.Length; i++) diff |= (computed[i] ^ hash[i]);
                return diff == 0;
            }
        }
    }
}
