namespace QuanLyQuanNet.DTOs
{
    class Computer
    {
        private string computerId;
        private string computerName;
        private string status;
        private string ipAddress;
        private double pricePerHour;

        public Computer()
        {
        }

        public Computer(string computerId, string computerName, string status, string ipAddress, double pricePerHour)
        {
            this.computerId = computerId;
            this.computerName = computerName;
            this.status = status;
            this.ipAddress = ipAddress;
            this.pricePerHour = pricePerHour;
        }

        public string ComputerId { get => this.computerId; set => this.computerId = value; }
        public string ComputerName { get => this.computerName; set => this.computerName = value; }
        public string Status { get => this.status; set => this.status = value; }
        public string IpAddress { get => this.ipAddress; set => this.ipAddress = value; }
        public double PricePerHour { get => this.pricePerHour; set => this.pricePerHour = value; }
    }
}
