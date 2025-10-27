using System;
using System.Data;

namespace QuanLyQuanNet.DAO
{
    public class UserDAO
    {
        private static UserDAO instance;
        public static UserDAO Instance { get { if (instance == null) instance = new UserDAO(); return instance; } }
        private UserDAO() { }

        public DataTable GetStaffList(string search = null)
        {
            string query = @"SELECT Id, Username, FullName, Phone, Email, Role, IsActive, CreatedAt, LastLogin
FROM Users
WHERE Role IN ('STAFF','MANAGER','CASHIER')";
            if (!string.IsNullOrEmpty(search))
            {
                query += " AND (Username LIKE @search OR FullName LIKE @search)";
                return DataProvider.Instance.ExecuteQuery(query, new object[] { "%" + search + "%" });
            }
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public int CreateStaff(string username, string passwordHash, string fullName, string phone, string email, string role, bool isActive)
        {
            string query = @"INSERT INTO Users (Username, PasswordHash, FullName, Phone, Email, Role, IsActive, CreatedAt)
VALUES (@username, @passwordHash, @fullName, @phone, @email, @role, @isActive, GETDATE())";
            object[] param = new object[] { username, passwordHash, fullName, phone, email, role, isActive };
            return DataProvider.Instance.ExecuteNonQuery(query, param);
        }

        public int UpdateStaff(int id, string fullName, string phone, string email, string role, bool isActive)
        {
            string query = @"UPDATE Users SET FullName = @fullName, Phone = @phone, Email = @email, Role = @role, IsActive = @isActive WHERE Id = @id";
            object[] param = new object[] { fullName, phone, email, role, isActive, id };
            return DataProvider.Instance.ExecuteNonQuery(query, param);
        }

        public int DeleteUser(int id)
        {
            string query = "DELETE FROM Users WHERE Id = @id";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { id });
        }

        public int ResetPassword(int id, string passwordHash)
        {
            string query = "UPDATE Users SET PasswordHash = @passwordHash WHERE Id = @id";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { passwordHash, id });
        }

        public int SetActive(int id, bool isActive)
        {
            string query = "UPDATE Users SET IsActive = @isActive WHERE Id = @id";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { isActive, id });
        }
    }
}
