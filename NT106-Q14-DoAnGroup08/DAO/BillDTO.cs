using System;

namespace QuanLyQuanNet.DAO
{
    public class BillDTO
    {
        public int ID { get; set; }
        public DateTime DateCheckout { get; set; }
        public int? StaffID { get; set; }
        public string StaffName { get; set; }
        public int Type { get; set; } // 1 = Thu, 0 = Chi
        public decimal TotalFromMachine { get; set; }
        public decimal MaintenanceFee { get; set; }
        public decimal FoodTotal { get; set; }
        public decimal Total
        {
            get { return TotalFromMachine + MaintenanceFee + FoodTotal; }
        }
    }
}