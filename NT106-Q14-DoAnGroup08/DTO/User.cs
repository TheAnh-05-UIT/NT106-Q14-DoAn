using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanNet.DTOs
{
    public class User
    {
        private string userId;
        private string username;
        private string password;
        private string fullName;
        private string phone;
        private string email;
        private string role; // "ADMIN", "EMPLOYEE", "CUSTOMER"
        public bool isActive;

        public User()
        {
        }

        public User(string userId, string username, string password, string role, bool isActive)
        {
            this.userId = userId;
            this.username = username;
            this.password = password;
            this.role = role;
            this.isActive = isActive;
        }

        public User(string userId, string username, string password, 
                    string fullName, string phone, string email, 
                    string role, bool isActive)
        {
            this.userId = userId;
            this.username = username;
            this.password = password;
            this.fullName = fullName;
            this.phone = phone;
            this.email = email;
            this.role = role;
            this.isActive = isActive;
        }
        public string Id { get => this.userId; set => this.userId = value; }
        public string UserName { get => this.username; set => this.username = value; }
        public string Password { get => this.password; set => this.password = value; }
        public string FullName { get => this.fullName;  set => this.fullName = value; }
        public string Phone { get => this.phone; set => this.phone = value; }
        public string Email { get => this.email; set => this.email = value; }
        public string Role { get => this.role; set => this.role = value; }
        public bool IsActive { get => this.isActive; set => this.isActive = value; }
    }
}
