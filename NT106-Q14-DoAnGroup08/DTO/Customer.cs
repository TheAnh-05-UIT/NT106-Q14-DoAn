using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanNet.DTOs
{
    public class Customer : User
    {
        private double balance;
        private DateTime registerDate;

        public Customer() : base()
        { }

        public Customer(string userId, string username, string password,
                        string fullName, string phone, string email,
                        string role, bool isActive,
                        double balance, DateTime registerDate)
               : base(userId, username, password, fullName, phone, email, role, isActive)
        {
            this.balance = balance;
            this.registerDate = registerDate;
        }

        public double Balance { get => this.balance; set => this.balance = value; }

        public DateTime RegisterDate { get => this.registerDate; set => this.registerDate = value; }
    }
}