using System;

namespace QuanLyQuanNet.DTOs
{
    class topUpTransaction
    {
        private string transactionId;
        private string customerId;
        private double amount; // So tien nap
        private DateTime date;
        private string employeeId;

        public topUpTransaction()
        {
        }

        public topUpTransaction(string transactionId, string customerId, double amount, DateTime date, string employeeId)
        {
            this.transactionId = transactionId;
            this.customerId = customerId;
            this.amount = amount;
            this.date = date;
            this.employeeId = employeeId;
        }

        public string TransactionId { get => this.transactionId; set => this.transactionId = value; }
        public string CustomerId { get => this.customerId; set => this.customerId = value; }
        public double Amount { get => this.amount; set => this.amount = value; }
        public DateTime Date { get => this.date; set => this.date = value; }
        public string EmployeeId { get => this.employeeId; set => this.employeeId = value; }
    }
}
