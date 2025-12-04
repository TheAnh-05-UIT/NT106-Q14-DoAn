using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TcpServer;

namespace TcpServer.Handlers
{
    public class HandlerRevenue
    {
        private readonly DatabaseHelper db;

        public class RevenueFilterRequest
        {
            public int Mode { get; set; } // 0: Day, 1: Week, 2: Month, 3: Year
            public DateTime SelectedDate { get; set; }
        }

        public class RevenueResponse
        {
            public DataTable Details { get; set; }
            public DataTable ChartData { get; set; }
            public string ChartTitle { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public decimal TotalRevenue { get; set; }
        }

        public HandlerRevenue(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }

        public object HandleRevenueFilter(dynamic data)
        {
            try
            {
                int mode = (int)data.Mode;
                DateTime selectedDate = (DateTime)data.SelectedDate;

                RevenueResponse response;

                switch (mode)
                {
                    case 0: // Day
                        response = GetRevenueByDay(selectedDate);
                        break;
                    case 1: // Week
                        response = GetRevenueByWeek(selectedDate);
                        break;
                    case 2: // Month
                        response = GetRevenueByMonth(selectedDate);
                        break;
                    case 3: // Year
                        response = GetRevenueByYear(selectedDate);
                        break;
                    default:
                        return new { status = "error", message = "Invalid mode selected" };
                }

                return new
                {
                    status = "success",
                    data = new
                    {
                        response.Details,
                        response.ChartData,
                        response.ChartTitle,
                        response.StartDate,
                        response.EndDate,
                        response.TotalRevenue
                    }
                };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }


        private RevenueResponse GetRevenueByDay(DateTime date)
        {
            SqlParameter[] prms = {
                new SqlParameter("@ngay", date.Day),
                new SqlParameter("@thang", date.Month),
                new SqlParameter("@nam", date.Year)
            };
            string queryDetails = @"SELECT CreatedAt AS [Ngày],
                                           InvoiceId AS [Mã HĐ],
                                           CustomerId AS [Khách hàng],
                                           TotalAmount AS [Số tiền (VND)]
                                    FROM Invoices
                                    WHERE YEAR(CreatedAt) = @nam
                                      AND MONTH(CreatedAt) = @thang
                                      AND DAY(CreatedAt) = @ngay";
            DataTable details = db.ExecuteQuery(queryDetails, prms);
            decimal totalRevenue = details.AsEnumerable().Sum(row => row.Field<decimal>("Số tiền (VND)"));

            return new RevenueResponse
            {
                Details = details,
                ChartData = details, 
                ChartTitle = $"Doanh thu ngày {date.ToShortDateString()}",
                StartDate = date.Date,
                EndDate = date.Date,
                TotalRevenue = totalRevenue
            };
        }

        private RevenueResponse GetRevenueByWeek(DateTime date)
        {
            int dayOfWeek = (int)date.DayOfWeek;
            if (dayOfWeek == 0) dayOfWeek = 7;
            DateTime startOfWeek = date.Date.AddDays(-(dayOfWeek - 1));
            DateTime endOfWeek = startOfWeek.AddDays(6);
            SqlParameter[] prmsDetails = {
        new SqlParameter("@NgayBatDau", startOfWeek.ToString("yyyy-MM-dd")),
        new SqlParameter("@NgayKetThuc", endOfWeek.ToString("yyyy-MM-dd"))
    };
            string queryDetails = @"SELECT CreatedAt AS [Ngày],
                                   InvoiceId AS [Mã HĐ],
                                   CustomerId AS [Khách hàng],
                                   TotalAmount AS [Số tiền (VND)]
                            FROM Invoices
                            WHERE CreatedAt >= @NgayBatDau
                              AND CreatedAt < DATEADD(day, 1, @NgayKetThuc)
                            ORDER BY CreatedAt";
            DataTable details = db.ExecuteQuery(queryDetails, prmsDetails); 
            SqlParameter[] prmsChart = {
        new SqlParameter("@NgayBatDau", startOfWeek.ToString("yyyy-MM-dd")),
        new SqlParameter("@NgayKetThuc", endOfWeek.ToString("yyyy-MM-dd"))
    };
            string queryChart = @"SELECT CAST(CreatedAt AS DATE) AS [Ngày],
                                 SUM(TotalAmount) AS [Tổng tiền]
                          FROM Invoices
                          WHERE CreatedAt >= @NgayBatDau
                            AND CreatedAt < DATEADD(day, 1, @NgayKetThuc)
                          GROUP BY CAST(CreatedAt AS DATE)
                          ORDER BY [Ngày]";
            DataTable chartData = db.ExecuteQuery(queryChart, prmsChart); 

            decimal totalRevenue = details.AsEnumerable().Sum(row => row.Field<decimal>("Số tiền (VND)"));

            return new RevenueResponse
            {
                Details = details,
                ChartData = chartData,
                ChartTitle = $"Doanh thu Tuần ({startOfWeek.ToShortDateString()} - {endOfWeek.ToShortDateString()})",
                StartDate = startOfWeek,
                EndDate = endOfWeek,
                TotalRevenue = totalRevenue
            };
        }

        private RevenueResponse GetRevenueByMonth(DateTime date)
        {
            SqlParameter[] prmsDetails = {
        new SqlParameter("@thang", date.Month),
        new SqlParameter("@nam", date.Year)
    };
            string queryDetails = @"SELECT CreatedAt AS [Ngày],
                                   InvoiceId AS [Mã HĐ],
                                   CustomerId AS [Khách hàng],
                                   TotalAmount AS [Số tiền (VND)]
                            FROM Invoices
                            WHERE YEAR(CreatedAt) = @nam
                              AND MONTH(CreatedAt) = @thang
                            ORDER BY CreatedAt";
            DataTable details = db.ExecuteQuery(queryDetails, prmsDetails); 

            SqlParameter[] prmsChart = {
        new SqlParameter("@thang", date.Month),
        new SqlParameter("@nam", date.Year)
    };
            string queryChart = @"SELECT DAY(CreatedAt) AS [Ngày],
                                 SUM(TotalAmount) AS [Tổng tiền]
                          FROM Invoices
                          WHERE YEAR(CreatedAt) = @nam
                            AND MONTH(CreatedAt) = @thang
                          GROUP BY DAY(CreatedAt)
                          ORDER BY [Ngày]";
            DataTable chartData = db.ExecuteQuery(queryChart, prmsChart); 

            decimal totalRevenue = details.AsEnumerable().Sum(row => row.Field<decimal>("Số tiền (VND)"));

            return new RevenueResponse
            {
                Details = details,
                ChartData = chartData,
                ChartTitle = $"Doanh thu Tháng {date.Month}/{date.Year}",
                StartDate = new DateTime(date.Year, date.Month, 1),
                EndDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month)),
                TotalRevenue = totalRevenue
            };
        }

        private RevenueResponse GetRevenueByYear(DateTime date)
        {
            SqlParameter[] prmsDetails = { new SqlParameter("@nam", date.Year) };
            string queryDetails = @"SELECT CreatedAt AS [Ngày],
                                   InvoiceId AS [Mã HĐ],
                                   CustomerId AS [Khách hàng],
                                   TotalAmount AS [Số tiền (VND)]
                            FROM Invoices
                            WHERE YEAR(CreatedAt) = @nam
                            ORDER BY CreatedAt";
            DataTable details = db.ExecuteQuery(queryDetails, prmsDetails); 
            SqlParameter[] prmsChart = { new SqlParameter("@nam", date.Year) };
            string queryChart = @"SELECT MONTH(CreatedAt) AS [Tháng],
                                 SUM(TotalAmount) AS [Tổng tiền]
                          FROM Invoices
                          WHERE YEAR(CreatedAt) = @nam
                          GROUP BY MONTH(CreatedAt)
                          ORDER BY [Tháng]";
            DataTable chartData = db.ExecuteQuery(queryChart, prmsChart); 

            decimal totalRevenue = details.AsEnumerable().Sum(row => row.Field<decimal>("Số tiền (VND)"));

            return new RevenueResponse
            {
                Details = details,
                ChartData = chartData,
                ChartTitle = $"Doanh thu Năm {date.Year}",
                StartDate = new DateTime(date.Year, 1, 1),
                EndDate = new DateTime(date.Year, 12, 31),
                TotalRevenue = totalRevenue
            };
        }
    }
}