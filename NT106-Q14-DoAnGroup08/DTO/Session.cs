using System;

namespace QuanLyQuanNet.DTOs
{
    class Session
    {
        private string sessionId;
        private string customerId;
        private string computerId;
        private DateTime startTime;
        private DateTime endTime;
        private double totalCost;

        public Session()
        {
        }

        public Session(string sessionId, string customerId, string computerId, DateTime startTime, DateTime endTime, double totalCost)
        {
            this.sessionId = sessionId;
            this.customerId = customerId;
            this.computerId = computerId;
            this.startTime = startTime;
            this.endTime = endTime;
            this.totalCost = totalCost;
        }

        public string SessionId { get => this.sessionId; set => this.sessionId = value; }
        public string CustomerId { get => this.customerId; set => this.customerId = value; }
        public DateTime StartTime { get => this.startTime; set => this.startTime = value; }
        public DateTime EndTime { get => this.endTime; set => this.endTime = value; }
        public double TotalCost { get => this.totalCost; set => this.totalCost = value; }
    }
}


