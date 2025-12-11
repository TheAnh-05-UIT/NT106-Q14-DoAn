using System;
using System.Collections.Generic;
using System.Data;

namespace QuanLyQuanNet.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;
        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return instance; }
            private set { instance = value; }
        }
        private BillDAO() { }

        // L?y danh s�ch nh�n vi�n ?? fill combobox filter
        public DataTable GetStaffList()
        {
            string query = "SELECT ID, FullName FROM Staff";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // L?y danh s�ch h�a ??n theo filter, tr? v? DataTable ?? bind tr?c ti?p
        public DataTable GetBills(DateTime from, DateTime to, int? staffId = null, int? type = null)
        {
            string sql = @"
SELECT b.ID,
       b.DateCheckout AS [Ngày thanh toán],
       ISNULL(s.FullName, '') AS [Nhân viên],
       CASE WHEN b.Type = 1 THEN 'Thu' ELSE 'Chi' END AS [Kiểu hóa đơn],
       ISNULL(b.TotalFromMachine,0) AS [Thu từ máy],
       ISNULL(b.MaintenanceFee,0) AS [Phí bảo trì],
       ISNULL(b.FoodTotal,0) AS [Tổng thu/chi thức ăn],
       (ISNULL(b.TotalFromMachine,0)+ISNULL(b.MaintenanceFee,0)+ISNULL(b.FoodTotal,0)) AS [Tổng thu/chi]
FROM Bill b
LEFT JOIN Staff s ON b.StaffID = s.ID
WHERE b.DateCheckout >= @fromDate AND b.DateCheckout <= @toDate
";
            var parameters = new List<object> { from, to };

            if (staffId.HasValue)
            {
                sql += " AND b.StaffID = @staffId";
                parameters.Add(staffId.Value);
            }

            if (type.HasValue)
            {
                sql += " AND b.Type = @typeFilter";
                parameters.Add(type.Value);
            }

            return DataProvider.Instance.ExecuteQuery(sql, parameters.ToArray());
        }

        // L?y t?ng Thu/Chi ?? th?ng k�
        public DataTable GetAggregates(DateTime from, DateTime to, int? staffId = null)
        {
            string sql = @"
SELECT 
    SUM(CASE WHEN b.Type = 1 THEN (ISNULL(b.TotalFromMachine,0)+ISNULL(b.MaintenanceFee,0)+ISNULL(b.FoodTotal,0)) ELSE 0 END) AS TotalThu,
    SUM(CASE WHEN b.Type = 0 THEN (ISNULL(b.TotalFromMachine,0)+ISNULL(b.MaintenanceFee,0)+ISNULL(b.FoodTotal,0)) ELSE 0 END) AS TotalChi
FROM Bill b
WHERE b.DateCheckout >= @fromDate AND b.DateCheckout <= @toDate
";
            var parameters = new List<object> { from, to };

            if (staffId.HasValue)
            {
                sql += " AND b.StaffID = @staffId";
                parameters.Add(staffId.Value);
            }

            return DataProvider.Instance.ExecuteQuery(sql, parameters.ToArray());
        }
    }
}