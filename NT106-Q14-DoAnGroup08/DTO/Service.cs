namespace QuanLyQuanNet.DTOs
{
    class Service
    {
        private string serviceId;
        private string serviceName;
        private double price;
        private string category; // Loai Hang
        private bool available;

        public Service()
        {
        }

        public Service(string serviceId, string serviceName, double price, string category, bool available)
        {
            this.serviceId = serviceId;
            this.serviceName = serviceName;
            this.price = price;
            this.category = category;
            this.available = available;
        }

        public string ServiceId { get => this.serviceId; set => this.serviceId = value; }
        public string ServiceName { get => this.serviceName; set => this.serviceName = value; }
        public double Price { get => this.price; set => this.price = value; }
        public string Category { get => this.category; set => this.category = value; }
        public bool IsAvailable { get => this.available; set => this.available = value; }
    }
}

