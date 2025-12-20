using System;
using System.Collections.Generic;

namespace QuanLyQuanNet.DTOs
{

    // Hoa don sau khi dung dich vu
    class Invoice
    {
        private string invoiceId;
        private string sessionId;
        private string customerId;
        private DateTime createdAt;
        private double total;
        private List<Service> services;

        public Invoice()
        {
        }

        public Invoice(string invoiceId, string sessionId, string customerId, DateTime createdAt, double total, List<Service> services)
        {
            this.invoiceId = invoiceId;
            this.sessionId = sessionId;
            this.customerId = customerId;
            this.createdAt = createdAt;
            this.total = total;
            this.services = services;
        }

        public string InvoiceId { get => this.invoiceId; set => this.invoiceId = value; }

        public string SessionId { get => this.sessionId; set => this.sessionId = value; }
        public string CustomerId { get => this.customerId; set => this.customerId = value; }
        public DateTime CreateAt { get => this.createdAt; set => this.createdAt = value; }
        public double Total { get => this.total; set => this.total = value; }
        public List<Service> Services { get => this.services; }
    }

}

