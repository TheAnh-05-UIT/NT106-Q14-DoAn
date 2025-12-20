using System;

namespace QuanLyQuanNet.DTOs
{
    class EmployeeDTO : User
    {
        private string employeeId;
        private DateTime hiredDate;
        private double salary;

        public EmployeeDTO()
        {
        }

        public EmployeeDTO(string userId, string username, string password,
                        string fullName, string phone, string email,
                        string role, bool isActive, string employeeId,
                        DateTime hiredDate, double salary)
                        : base(userId, username, password, fullName, phone, email, role, isActive)
        {
            this.employeeId = employeeId;
            this.hiredDate = hiredDate;
            this.salary = salary;
        }

        public string EmployeeId { get => this.employeeId; set => this.employeeId = value; }

        public DateTime HiredDate { get => this.hiredDate; set => this.hiredDate = value; }

        public double Salary { get => this.salary; set => this.salary = value; }
    }
}
