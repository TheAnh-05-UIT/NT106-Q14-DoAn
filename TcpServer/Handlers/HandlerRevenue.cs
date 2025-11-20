using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TcpServer;
namespace TcpServer.Handlers
{
    
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
    
    public class RevenueService
    {
        private readonly DatabaseHelper dbHelper;
        public RevenueService(string connectionString)
        {
            dbHelper = new DatabaseHelper(connectionString);
        }
        public RevenueResponse GetRevenueByDay(DateTime date)
        {
            SqlParameter[] prms = {
                new SqlParameter("@ngay", date.Day.ToString()),
                new SqlParameter("@thang", date.Month.ToString()),
                new SqlParameter("@nam", date.Year.ToString())
            };
            string queryDetails = @"SELECT CreatedAt AS [Ngày],
                                           InvoiceId AS [Mã HĐ],
                                           CustomerId AS [Khách hàng],
                                           TotalAmount AS [Số tiền (VND)]
                                    FROM Invoices
                                    WHERE YEAR(CreatedAt) = @nam
                                      AND MONTH(CreatedAt) = @thang
                                      AND DAY(CreatedAt) = @ngay";
            DataTable details = dbHelper.ExecuteQuery(queryDetails, prms);
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
        public RevenueResponse GetRevenueByWeek(DateTime date)
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
            DataTable details = dbHelper.ExecuteQuery(queryDetails, prmsDetails);
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
            DataTable chartData = dbHelper.ExecuteQuery(queryChart, prmsChart);
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
        public RevenueResponse GetRevenueByMonth(DateTime date)
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
            DataTable details = dbHelper.ExecuteQuery(queryDetails, prmsDetails);
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
            DataTable chartData = dbHelper.ExecuteQuery(queryChart, prmsChart);
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
        public RevenueResponse GetRevenueByYear(DateTime date)
        {
            SqlParameter[] prmsDetails = {
                new SqlParameter("@nam", date.Year)
            };
            string queryDetails = @"SELECT CreatedAt AS [Ngày],
                                           InvoiceId AS [Mã HĐ],
                                           CustomerId AS [Khách hàng],
                                           TotalAmount AS [Số tiền (VND)]
                                    FROM Invoices
                                    WHERE YEAR(CreatedAt) = @nam
                                    ORDER BY CreatedAt";
            DataTable details = dbHelper.ExecuteQuery(queryDetails, prmsDetails);
            SqlParameter[] prmsChart = {
                new SqlParameter("@nam", date.Year)
            };
            string queryChart = @"SELECT MONTH(CreatedAt) AS [Tháng],
                                         SUM(TotalAmount) AS [Tổng tiền]
                                  FROM Invoices
                                  WHERE YEAR(CreatedAt) = @nam
                                  GROUP BY MONTH(CreatedAt)
                                  ORDER BY [Tháng]";
            DataTable chartData = dbHelper.ExecuteQuery(queryChart, prmsChart);
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
    // Handler Layer - Orchestrates business logic
    public class RevenueHandler
    {
        private readonly RevenueService revenueService;
        public RevenueHandler(string connectionString)
        {
            revenueService = new RevenueService(connectionString);
        }
        public RevenueResponse HandleRevenueFilter(RevenueFilterRequest request)
        {
            switch (request.Mode)
            {
                case 0: // Day
                    return revenueService.GetRevenueByDay(request.SelectedDate);
                case 1: // Week
                    return revenueService.GetRevenueByWeek(request.SelectedDate);
                case 2: // Month
                    return revenueService.GetRevenueByMonth(request.SelectedDate);
                case 3: // Year
                    return revenueService.GetRevenueByYear(request.SelectedDate);
                default:
                    throw new ArgumentException("Invalid mode selected");
            }
        }
        
        public RevenueResponse HandleRefresh()
        {
            return new RevenueResponse
            {
                Details = new DataTable(),
                ChartData = new DataTable(),
                ChartTitle = string.Empty,
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MinValue,
                TotalRevenue = 0
            };
        }
        public static int GetWeekOfYear(DateTime time)
        {
            DayOfWeek day = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar.GetDayOfWeek(time);
            if (day == DayOfWeek.Sunday)
            {
                day = (DayOfWeek)7;
            }
            if ((int)day >= 1 && (int)day <= 3)
            {
                time = time.AddDays(3);
            }
            int week = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(
                time,
                System.Globalization.CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday
            );
            return week;
        }
    }
   
    public class ChartDataHelper
    {
        public static Dictionary<string, int> PrepareChartDataForDay(DataTable dt)
        {
            var result = new Dictionary<string, int>();
            var groupedData = dt.AsEnumerable()
                                .GroupBy(row => row.Field<DateTime>("Ngày").Hour)
                                .Select(g => new {
                                    Gio = g.Key,
                                    TongTien = g.Sum(row => Convert.ToInt32(row["Số tiền (VND)"]))
                                })
                                .OrderBy(x => x.Gio);
            foreach (var item in groupedData)
            {
                result.Add($"{item.Gio}h", item.TongTien);
            }
            if (!result.Any())
            {
                result.Add("Không có dữ liệu", 0);
            }
            return result;
        }
        public static Dictionary<string, int> PrepareChartDataForWeek(DataTable dt, DateTime startOfWeek)
        {
            var result = new Dictionary<string, int>();
            string[] dayNames = { "Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy", "Chủ Nhật" };
            
            for (int i = 0; i < 7; i++)
            {
                DateTime date = startOfWeek.AddDays(i);
                result.Add($"{dayNames[i]} ({date.Day}/{date.Month})", 0);
            }
           
            foreach (DataRow row in dt.Rows)
            {
                DateTime date = Convert.ToDateTime(row["Ngày"]);
                int revenue = Convert.ToInt32(row["Tổng tiền"]);
                int dayIndex = ((int)date.DayOfWeek - 1 + 7) % 7;
                string key = $"{dayNames[dayIndex]} ({date.Day}/{date.Month})";
                if (result.ContainsKey(key))
                {
                    result[key] = revenue;
                }
            }
            return result;
        }
        public static Dictionary<int, int> PrepareChartDataForMonth(DataTable dt, int year, int month)
        {
            var result = new Dictionary<int, int>();
            int daysInMonth = DateTime.DaysInMonth(year, month);
          
            for (int i = 1; i <= daysInMonth; i++)
            {
                result.Add(i, 0);
            }
          
            foreach (DataRow row in dt.Rows)
            {
                int day = Convert.ToInt32(row["Ngày"]);
                int revenue = Convert.ToInt32(row["Tổng tiền"]);
                if (result.ContainsKey(day))
                {
                    result[day] = revenue;
                }
            }
            return result;
        }
        public static Dictionary<string, int> PrepareChartDataForYear(DataTable dt)
        {
            var result = new Dictionary<string, int>();
            string[] monthNames = { "", "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
                                   "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
            
            for (int i = 1; i <= 12; i++)
            {
                result.Add(monthNames[i], 0);
            }
          
            foreach (DataRow row in dt.Rows)
            {
                int month = Convert.ToInt32(row["Tháng"]);
                int revenue = Convert.ToInt32(row["Tổng tiền"]);
                result[monthNames[month]] = revenue;
            }
            return result;
        }
    }
}